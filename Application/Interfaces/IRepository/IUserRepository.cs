using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Interfaces.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> getUserByEmailAsync(string Email);

    }
}
