namespace Vivigest_backend.Application.DTOs.DocumentType
{
    public class DocumentTypeGetAllResponseDto
    {
        public IEnumerable<DocumentTypeDto> DocumentTypes { get; set; } = new List<DocumentTypeDto>();
    }
}
