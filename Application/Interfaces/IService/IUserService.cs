using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<Result<(UserResponseDto User, string Token)>> loginAsync(LoginRequestDto request);
        Task<Result<(RegisterUserResponseDto User, string Token)>> registerAsync(RegisterUserRequestDto request);
    }
}
