using FluentValidation;
using Vivigest_backend.Application.DTOs.Users;

namespace Vivigest_backend.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.IdDocumentType)
                .GreaterThan(0).WithMessage("El ID del tipo de documento es inválido");

            RuleFor(x => x.NitNumber)
                .GreaterThan(0).WithMessage("El número de documento debe ser positivo")
                .Must(x => x.ToString().Length >= 5 && x.ToString().Length <= 15)
                .WithMessage("El número de documento debe tener entre 5 y 15 dígitos.");

            RuleFor(x => x.Names)
                .NotEmpty().WithMessage("El nombre es requerido")
                .Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios");

            RuleFor(x => x.LastNames)
                .NotEmpty().WithMessage("Los apellidos son requeridos")
                .Length(3, 50).WithMessage("Los apellidos deben tener entre 3 y 50 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("Los apellidos solo pueden contener letras y espacios");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El teléfono es requerido")
                .Matches(@"^\+?[0-9]{10,14}$").WithMessage("El teléfono debe tener un formato válido (ej. +573000000000 o 3000000000) y entre 10 a 15 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido")
                .MaximumLength(100).WithMessage("El email no puede exceder 100 caracteres")
                .EmailAddress().WithMessage("Debe ser un email válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una minúscula")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");
        }
    }
}
