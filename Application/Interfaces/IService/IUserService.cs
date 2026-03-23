using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<Result<(UserResponseDto User, string Token, string RefreshToken)>> loginAsync(LoginRequestDto request);
        Task<Result<(RegisterUserResponseDto User, string Token, string RefreshToken)>> registerAsync(RegisterUserRequestDto request);
        Task<Result<(string Token, string RefreshToken)>> refreshTokenAsync(string CurrentRefreshToken);
    }
}
