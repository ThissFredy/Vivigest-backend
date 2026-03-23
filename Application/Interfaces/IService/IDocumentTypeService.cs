using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.DocumentType;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    /// <summary>
    /// Service interface for managing document types.
    /// </summary>
    public interface IDocumentTypeService
    {
        /// <summary>
        /// Retrieves all document types.
        /// </summary>
        /// <returns>A result containing a list of document types.</returns>
        Task<Result<DocumentTypeGetAllResponseDto>> getAllDocumentTypeService();

        /// <summary>
        /// Creates a new document type.
        /// </summary>
        /// <param name="request">The request containing the document type details.</param>
        /// <returns>A result containing the created document type.</returns>
        Task<Result<DocumentTypeAddResponseDto>> addDocumentTypeService(DocumentTypeRequestDto request);

        /// <summary>
        /// Retrieves a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type.</param>
        /// <returns>A result containing the document type details.</returns>
        Task<Result<DocumentTypeGetByIdResponseDto>> getDocumentTypeByIdService(int id);

        /// <summary>
        /// Updates an existing document type.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to update.</param>
        /// <param name="request">The request containing the updated details.</param>
        /// <returns>A result containing the updated document type.</returns>
        Task<Result<DocumentTypeUpdateResponseDto>> updateDocumentTypeService(int id, DocumentTypeUpdateRequestDto request);

        /// <summary>
        /// Deletes a document type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the document type to delete.</param>
        /// <returns>A result containing a success message.</returns>
        Task<Result<DocumentTypeDeleteResponseDto>> deleteDocumentTypeService(int id);
    }
}
