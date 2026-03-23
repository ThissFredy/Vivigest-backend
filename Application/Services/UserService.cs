using Azure.Identity;
using BCrypt.Net;
using System.Runtime.CompilerServices;
using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IAuth;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Application.Interfaces.IService;
using Vivigest_backend.Application.Utilities;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository, 
            IJwtProvider jwtProvider,
            IRefreshTokenRepository refreshToken)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _refreshTokenRepository = refreshToken;
        }

        /// <summary>
        /// Login user by email and password. If the credentials are valid, returns a JWT token.
        /// <param name="request">The login request containing email and password.</param>
        /// </summary>
        public async Task<Result<(UserResponseDto User, string Token, string RefreshToken)>> loginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.getUserByEmailAsync(request.Email);

            // Check if user exists
            if (user == null)
            {
                return Result<(UserResponseDto User, string Token, string RefreshToken)>.Failure(new Error("InvalidCredentials", "The email or password is incorrect."));
            }

            // Check if is active
            if (!user.Activated)
            {
                return Result<(UserResponseDto User, string Token, string RefreshToken)>.Failure(new Error("NotFound", "The user is not activated"));
            }

            // Valid password
            bool isPasswordValid = PasswordManager.verifyPasswordHash(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt
            );

            if (!isPasswordValid)
            {
                return Result<(UserResponseDto User, string Token, string RefreshToken)>.Failure(new Error("InvalidCredentials", "The email or password is incorrect."));
            }

            // Generate JWT token
            string generatedToken = _jwtProvider.Generate(user);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                IdUser = user.IdUser,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsExpired = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.addAsync(newRefreshToken);

            var response = new UserResponseDto
            {
                IdUser = user.IdUser,
                Names = user.Person.Names,
                LastNames = user.Person.LastNames,
                Email = user.Person.Email,
            };

            return Result<(UserResponseDto User, string Token, string RefreshToken)>.Success((response, generatedToken, refreshToken));
        }

        public async Task<Result<(RegisterUserResponseDto User, string Token, string RefreshToken)>> registerAsync(RegisterUserRequestDto request)
        {
            // Check if user already exists
            var existingUser = await _userRepository.getUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result<(RegisterUserResponseDto User, string Token, string RefreshToken)>.Failure(new Error("AlreadyExists", "A user with this email already exists."));
            }

            // Create Person
            var person = new Person
            {
                IdDocumentType = request.IdDocumentType,
                DocumentNumber = request.NitNumber.ToString(),
                Names = request.Names,
                LastNames = request.LastNames,
                Phone = request.PhoneNumber,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            var (passwordHash, passwordSalt) = PasswordManager.generatePassword(request.Password);

            // Create User
            var newUser = new User
            {
                Person = person,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Activated = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.addAsync(newUser);


            string generatedToken = _jwtProvider.Generate(createdUser);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                IdUser = createdUser.IdUser,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsExpired = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.addAsync(newRefreshToken);


            var response = new RegisterUserResponseDto
            {
                IdUser = createdUser.IdUser,
                FullName = $"{createdUser.Person.Names} {createdUser.Person.LastNames}",
                Email = createdUser.Person.Email,
                Role = "User",
            };

            return Result<(RegisterUserResponseDto User, string Token, string RefreshToken)>.Success((response, generatedToken, refreshToken));
        }

        public async Task<Result<(string Token, string RefreshToken)>> refreshTokenAsync(string CurrentRefreshToken)
        {
            var storedToken = await _refreshTokenRepository.getByTokenAsync(CurrentRefreshToken);

            if (storedToken == null)
            {
                return Result<(string Token, string RefreshToken)>.Failure(new Error("InvalidRefreshToken", "El token es invalido."));
            }

            if (storedToken.IsExpired || storedToken.Expires < DateTime.UtcNow)
            {
                return Result<(string, string)>.Failure(new Error("ExpiredToken", "El token de refresco ha expirado o fue revocado. Inicie sesión nuevamente."));
            }


            var user = await _userRepository.getByIdAsync(storedToken.IdUser);


            storedToken.IsExpired = true;
            await _refreshTokenRepository.updateAsync(storedToken);

            string newJwtToken = _jwtProvider.Generate(user);
            string newRefreshToken = _jwtProvider.GenerateRefreshToken();


            var newRefreshTokenEntity = new RefreshToken
            {
                IdUser = user.IdUser,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsExpired = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.addAsync(newRefreshTokenEntity);
            return Result<(string Token, string RefreshToken)>.Success((newJwtToken, newRefreshToken));
        }
    }
}
