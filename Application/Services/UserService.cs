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
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        /// <summary>
        /// Login user by email and password. If the credentials are valid, returns a JWT token.
        /// <param name="request">The login request containing email and password.</param>
        /// </summary>
        public async Task<Result<(UserResponseDto User, string Token)>> loginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.getUserByEmailAsync(request.Email);

            // Check if user exists
            if (user == null)
            {
                return Result<(UserResponseDto User, string Token)>.Failure(new Error("InvalidCredentials", "The email or password is incorrect."));
            }

            // Check if is active
            if (!user.Activated)
            {
                return Result<(UserResponseDto User, string Token)>.Failure(new Error("NotFound", "The user is not activated"));
            }

            // Valid password
            bool isPasswordValid = PasswordManager.verifyPasswordHash(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt
            );

            if (!isPasswordValid)
            {
                return Result<(UserResponseDto User, string Token)>.Failure(new Error("InvalidCredentials", "The email or password is incorrect."));
            }

            // Generate JWT token
            string generatedToken = _jwtProvider.Generate(user);

            var response = new UserResponseDto
            {
                IdUser = user.IdUser,
                Names = user.Person.Names,
                LastNames = user.Person.LastNames,
                Email = user.Person.Email,
            };

            return Result<(UserResponseDto User, string Token)>.Success((response, generatedToken));
        }

        public async Task<Result<(RegisterUserResponseDto User, string Token)>> registerAsync(RegisterUserRequestDto request)
        {
            // Check if user already exists
            var existingUser = await _userRepository.getUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result<(RegisterUserResponseDto User, string Token)>.Failure(new Error("AlreadyExists", "A user with this email already exists."));
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
            var response = new RegisterUserResponseDto
            {
                IdUser = createdUser.IdUser,
                FullName = $"{createdUser.Person.Names} {createdUser.Person.LastNames}",
                Email = createdUser.Person.Email,
                Role = "User",
            };

            return Result<(RegisterUserResponseDto User, string Token)>.Success((response, generatedToken));
        }
    }
}
