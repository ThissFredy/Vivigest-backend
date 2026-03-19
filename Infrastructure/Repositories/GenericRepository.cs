using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Infrastructure.Persistance;

namespace Vivigest_backend.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VivigestDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(VivigestDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> getByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> addAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task updateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task deleteAsync(int id)
        {
            var entity = await getByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
