using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<Result<UserRespondeDto>> LoginAsync(LoginRequestDto request);
    }
}
