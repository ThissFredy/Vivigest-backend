using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Interfaces.IAuth
{
    public interface IJwtProvider
    {
        string Generate(User user);
        string GenerateRefreshToken();
    }
}
