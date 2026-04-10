using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Interfaces.IRepository
{
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
        Task<bool> isDocumentNumberRegisteredAsync(string documentNumber);
    }
}
