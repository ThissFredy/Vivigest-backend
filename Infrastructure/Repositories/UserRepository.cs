using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Application.Interfaces;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Domain.Entities;
using Vivigest_backend.Infrastructure.Persistance;

namespace Vivigest_backend.Infrastructure.Repositories
{
    // Cambiamos a IUserRepository para que pueda incluir los métodos del genérico + el de Email
    public class UserRepository : IUserRepository
    {
        private readonly VivigestDbContext _context;

        public UserRepository(VivigestDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(p => p.UserRols)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Person.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(p => p.UserRols)
                    .ThenInclude(ur => ur.Rol)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(p => p.UserRols)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.IdUser == id);
        }

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}