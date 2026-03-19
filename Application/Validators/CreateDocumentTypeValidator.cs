using FluentValidation;
using Vivigest_backend.Application.DTOs.DocumentType;

namespace Vivigest_backend.Application.Validators
{
    public class CreateDocumentTypeValidator : AbstractValidator<DocumentTypeRequestDto>
    {
        public CreateDocumentTypeValidator()
        {
            RuleFor(x => x.NameDocumentType)
                .NotEmpty().WithMessage("El nombre del tipo de documento es requerido")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios");
        }
    }
}
