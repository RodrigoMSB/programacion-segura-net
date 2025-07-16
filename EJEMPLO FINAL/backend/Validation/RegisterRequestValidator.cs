using FluentValidation;
using SeguridadBancoFinal.DTOs;

namespace SeguridadBancoFinal.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Formato de email inválido.")
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("Debe incluir al menos una letra mayúscula.")
                .Matches("[a-z]").WithMessage("Debe incluir al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("Debe incluir al menos un número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Debe incluir al menos un símbolo.");

            RuleFor(x => x.Rol)
                .Must(r => r == "Cliente" || r == "Admin" || r == null)
                .WithMessage("El rol debe ser Cliente o Admin.");
        }
    }
}