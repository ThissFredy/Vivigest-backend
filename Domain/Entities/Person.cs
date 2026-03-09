namespace Vivigest_backend.Domain.Entities
{
    public class Person
    {
        public int IdPerson { get; set; }
        public int IdDocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Relations
        public DocumentType DocumentType { get; set; } = null!;

    }
}
