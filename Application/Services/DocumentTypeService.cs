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
    /// <summary>
    /// Service implementation for managing document types.
    /// </summary>
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IGenericRepository<DocumentType> _documentTypeRepository;

        public DocumentTypeService(IGenericRepository<DocumentType> documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        /// <summary>
        /// Retrieves all document types from the database.
        /// </summary>
        /// <returns>A result containing a list of document types.</returns>
        public async Task<Result<DocumentTypeGetAllResponseDto>> getAllDocumentTypeService()
        {
            // Fetch all items from the repository
            var documentTypes = await _documentTypeRepository.getAllAsync();

            // Map entities to DTOs
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

        /// <summary>
        /// Creates a new document type.
        /// </summary>
        /// <param name="request">The request containing the document type details.</param>
        /// <returns>A result containing the created document type.</returns>
        public async Task<Result<DocumentTypeAddResponseDto>> addDocumentTypeService(DocumentTypeRequestDto request)
        {
            var newDocumentType = new DocumentType
            {
                NameDocumentType = request.NameDocumentType
            };

            // Save the new entity
            var createdEntity = await _documentTypeRepository.addAsync(newDocumentType);

            var response = new DocumentTypeAddResponseDto
            {
                IdDocumentType = createdEntity.IdDocumentType,
                NameDocumentType = createdEntity.NameDocumentType
            };

            return Result<DocumentTypeAddResponseDto>.Success(response);
        }

        /// <summary>
        /// Retrieves a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type.</param>
        /// <returns>A result containing the document type details.</returns>
        public async Task<Result<DocumentTypeGetByIdResponseDto>> getDocumentTypeByIdService(int id)
        {
            var documentType = await _documentTypeRepository.getByIdAsync(id);

            // Check if it exists
            if (documentType == null)
            {
                return Result<DocumentTypeGetByIdResponseDto>.Failure(new Error("DocumentType.NotFound", "The document type was not found."));
            }

            var response = new DocumentTypeGetByIdResponseDto
            {
                IdDocumentType = documentType.IdDocumentType,
                NameDocumentType = documentType.NameDocumentType
            };

            return Result<DocumentTypeGetByIdResponseDto>.Success(response);
        }

        /// <summary>
        /// Updates an existing document type.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to update.</param>
        /// <param name="request">The request containing the updated details.</param>
        /// <returns>A result containing the updated document type.</returns>
        public async Task<Result<DocumentTypeUpdateResponseDto>> updateDocumentTypeService(int id, DocumentTypeUpdateRequestDto request)
        {
            var documentType = await _documentTypeRepository.getByIdAsync(id);

            // Check if it exists before updating
            if (documentType == null)
            {
                return Result<DocumentTypeUpdateResponseDto>.Failure(new Error("DocumentType.NotFound", "The document type was not found."));
            }

            documentType.NameDocumentType = request.NameDocumentType;
            await _documentTypeRepository.updateAsync(documentType);

            var response = new DocumentTypeUpdateResponseDto
            {
                IdDocumentType = documentType.IdDocumentType,
                NameDocumentType = documentType.NameDocumentType
            };

            return Result<DocumentTypeUpdateResponseDto>.Success(response);
        }

        /// <summary>
        /// Deletes a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to delete.</param>
        /// <returns>A result containing a success message.</returns>
        public async Task<Result<DocumentTypeDeleteResponseDto>> deleteDocumentTypeService(int id)
        {
            var documentType = await _documentTypeRepository.getByIdAsync(id);

            // Verify existence before deletion
            if (documentType == null)
            {
                return Result<DocumentTypeDeleteResponseDto>.Failure(new Error("DocumentType.NotFound", "The document type was not found."));
            }

            await _documentTypeRepository.deleteAsync(id);

            var response = new DocumentTypeDeleteResponseDto
            {
                IdDocumentType = id,
                Message = "Document type deleted successfully."
            };

            return Result<DocumentTypeDeleteResponseDto>.Success(response);
        }
    }
}
