namespace Vivigest_backend.Application.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> getAllAsync();
        Task<T?> getByIdAsync(int id);
        Task<T> addAsync(T entity);
        Task updateAsync(T entity);
        Task deleteAsync(int id);
    }
}
