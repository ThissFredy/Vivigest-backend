using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    /// <summary>
    /// Service interface for user authentication and management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user using email and password.
        /// </summary>
        /// <param name="request">The login request details.</param>
        /// <returns>A result containing the user information, JWT token, and refresh token.</returns>
        Task<Result<(UserResponseDto User, string Token, string RefreshToken)>> loginAsync(LoginRequestDto request);

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The registration details.</param>
        /// <returns>A result containing the registered user information, JWT token, and refresh token.</returns>
        Task<Result<(RegisterUserResponseDto User, string Token, string RefreshToken)>> registerAsync(RegisterUserRequestDto request);

        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <param name="CurrentRefreshToken">The current valid refresh token.</param>
        /// <returns>A result containing a new JWT token and a new refresh token.</returns>
        Task<Result<(string Token, string RefreshToken)>> refreshTokenAsync(string CurrentRefreshToken);
    }
}
