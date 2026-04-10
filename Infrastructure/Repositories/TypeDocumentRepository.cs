using Microsoft.EntityFrameworkCore;
using Vivigest_backend.Application.Interfaces;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Domain.Entities;
using Vivigest_backend.Infrastructure.Persistance;

namespace Vivigest_backend.Infrastructure.Repositories
{
    public class TypeDocumentRepository : IDocumentTypeRepository
    {
        private readonly VivigestDbContext _context;

        public TypeDocumentRepository(VivigestDbContext context)
        {
            _context = context;
        }

        public async Task<bool> isDocumentNumberRegisteredAsync(string documentNumber)
        {
            return await _context.Users
                .AnyAsync(u => u.Person != null && u.Person.DocumentNumber == documentNumber);
        }

        public async Task<IEnumerable<DocumentType>> getAllAsync()
        {
            return await _context.DocumentTypes
                .ToListAsync();
        }

        public async Task<DocumentType?> getByIdAsync(int id)
        {
            return await _context.DocumentTypes
                .FirstOrDefaultAsync(u => u.IdDocumentType == id);
        }


        public async Task<DocumentType> addAsync(DocumentType entity)
        {
            await _context.DocumentTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task updateAsync(DocumentType entity)
        {
            _context.DocumentTypes.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task deleteAsync(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);

            if (documentType != null)
            {
                _context.DocumentTypes.Remove(documentType);
                await _context.SaveChangesAsync();
            }
        }
    }
}