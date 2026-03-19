using Azure.Identity;
using BCrypt.Net;
using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.DocumentType;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IAuth;
using Vivigest_backend.Application.Interfaces.IRepository;
using Vivigest_backend.Application.Interfaces.IService;
using Vivigest_backend.Application.Utilities;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Application.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IGenericRepository<DocumentType> _documentTypeRepository;

        public DocumentTypeService(IGenericRepository<DocumentType> documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<Result<DocumentTypeGetAllResponseDto>> getAllDocumentTypeService()
        {
            var documentTypes = await _documentTypeRepository.getAllAsync();

            var dtos = documentTypes.Select(dt => new DocumentTypeDto
            {
                IdDocumentType = dt.IdDocumentType,
                NameDocumentType = dt.NameDocumentType
            }).ToList();

            var response = new DocumentTypeGetAllResponseDto
            {
                DocumentTypes = dtos
            };

            return Result<DocumentTypeGetAllResponseDto>.Success(response);
        }

        public async Task<Result<DocumentTypeAddResponseDto>> addDocumentTypeService(DocumentTypeRequestDto request)
        {
            var newDocumentType = new DocumentType
            {
                NameDocumentType = request.NameDocumentType
            };

            var createdEntity = await _documentTypeRepository.addAsync(newDocumentType);

            var response = new DocumentTypeAddResponseDto
            {
                IdDocumentType = createdEntity.IdDocumentType,
                NameDocumentType = createdEntity.NameDocumentType
            };

            return Result<DocumentTypeAddResponseDto>.Success(response);
        }
    }
}
