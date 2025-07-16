using FluentValidation;
using SeguridadBancoFinal.DTOs;

namespace SeguridadBancoFinal.Validation
{
    public class TransferenciaRequestValidator : AbstractValidator<TransferenciaRequest>
    {
        public TransferenciaRequestValidator()
        {
            RuleFor(x => x.CuentaOrigenId)
                .GreaterThan(0).WithMessage("Cuenta origen inválida.");

            RuleFor(x => x.CuentaDestinoId)
                .GreaterThan(0).WithMessage("Cuenta destino inválida.")
                .NotEqual(x => x.CuentaOrigenId).WithMessage("No se puede transferir a la misma cuenta.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que cero.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");
        }
    }
}