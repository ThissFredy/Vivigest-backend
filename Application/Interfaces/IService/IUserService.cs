using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<Result<UserResponseDto>> loginAsync(LoginRequestDto request);
        Task<Result<RegisterUserResponseDto>> registerAsync(RegisterUserRequestDto request);
    }
}
