using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Application.Interfaces;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Domain.Entities;
using Vivigest_backend.Infrastructure.Persistance;

namespace Vivigest_backend.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly VivigestDbContext _context;

        public RefreshTokenRepository(VivigestDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefreshToken>> getAllAsync()
        {
            return await _context.RefreshTokens
                .ToListAsync();
        }

        public async Task<RefreshToken?> getByIdAsync(int id)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(u => u.IdToken == id);
        }


        public async Task<RefreshToken> addAsync(RefreshToken entity)
        {
            await _context.RefreshTokens.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task updateAsync(RefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task deleteAsync(int id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken?> getByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(u => u.Token == token);
        }
    }
}