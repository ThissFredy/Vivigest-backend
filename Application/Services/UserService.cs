using Azure.Identity;
using BCrypt.Net;
using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IAuth;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Application.Interfaces.IService;

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

        public async Task<Result<UserRespondeDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            // Check if user exists
            if (user == null)
            {
                return Result<UserRespondeDto>.Failure(new Error("InvalidCredentials", "The email or password is incorrect."));
            }

            // Check if is active
            if (!user.Activated)
            {
                return Result<UserRespondeDto>.Failure(new Error("NotFound", "The user is not activated"));
            }

            // Check password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Result<UserRespondeDto>.Failure(new Error("NotFound", "The email or password is incorrect."));
            }

            // Generate JWT token
            string generatedToken = _jwtProvider.Generate(user);

            var response = new UserRespondeDto
            {
                IdUser = user.IdUser,
                FullName = $"{user.Person.Names}" + $" {user.Person.LastNames}",
                Email = user.Person.Email,
                Token = generatedToken,
            };
            return Result<UserRespondeDto>.Success(response);
        }
    }
}
