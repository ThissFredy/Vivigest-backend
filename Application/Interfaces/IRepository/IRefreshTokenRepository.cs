using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Interfaces.IRepository
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> getByTokenAsync(string token);
        Task revokeRefreshToken(string refreshToken);
    }
}
