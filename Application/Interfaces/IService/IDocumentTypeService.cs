using Vivigest_backend.Application.Common;
using Vivigest_backend.Application.DTOs.DocumentType;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Interfaces.IService
{
    public interface IDocumentTypeService
    {
        Task<Result<DocumentTypeGetAllResponseDto>> getAllDocumentTypeService();
        Task<Result<DocumentTypeAddResponseDto>> addDocumentTypeService(DocumentTypeRequestDto request);
    }
}
