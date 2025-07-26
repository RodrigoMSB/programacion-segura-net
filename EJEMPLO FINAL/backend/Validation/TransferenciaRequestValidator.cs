// ================================================================================================
// TransferenciaRequestValidator.cs — Validador para Transferencias Bancarias
// ================================================================================================
// Esta clase valida las solicitudes de transferencia entre cuentas, asegurando que:
// - Las cuentas origen y destino sean válidas y distintas.
// - El monto transferido sea mayor a cero.
// - La descripción, si se provee, tenga una longitud controlada.
//
// Esta validación evita operaciones erróneas o maliciosas desde el front-end
// y refuerza la lógica de negocio antes de llegar a la capa de persistencia.
//
// --------------------------------------------------------------------------------
// IMPORTS — Librerías requeridas para validación de DTOs
// --------------------------------------------------------------------------------
using FluentValidation;                         // Librería para definir reglas de validación limpias y expresivas.
using SeguridadBancoFinal.DTOs;                 // Contiene la definición del DTO TransferenciaRequest.

// --------------------------------------------------------------------------------
// ESPACIO DE NOMBRES
// --------------------------------------------------------------------------------
namespace SeguridadBancoFinal.Validation
{
    /// <summary>
    /// Validador para el DTO TransferenciaRequest.
    /// Garantiza integridad de datos en las transferencias entre cuentas.
    /// </summary>
    public class TransferenciaRequestValidator : AbstractValidator<TransferenciaRequest>
    {
        /// <summary>
        /// Constructor que define las reglas de validación aplicables a una transferencia.
        /// </summary>
        public TransferenciaRequestValidator()
        {
            // ==========================================================================
            // VALIDACIÓN: Cuenta Origen
            // ==========================================================================
            RuleFor(x => x.CuentaOrigenId)
                .GreaterThan(0).WithMessage("Cuenta origen inválida."); 
                // La cuenta origen debe ser un ID positivo válido.

            // ==========================================================================
            // VALIDACIÓN: Cuenta Destino
            // ==========================================================================
            RuleFor(x => x.CuentaDestinoId)
                .GreaterThan(0).WithMessage("Cuenta destino inválida.")
                .NotEqual(x => x.CuentaOrigenId)
                .WithMessage("No se puede transferir a la misma cuenta.");
                // Evita transferencias entre la misma cuenta (fraude o error frecuente).

            // ==========================================================================
            // VALIDACIÓN: Monto
            // ==========================================================================
            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que cero.");
                // Asegura montos válidos y evita transacciones sin valor.

            // ==========================================================================
            // VALIDACIÓN: Descripción
            // ==========================================================================
            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");
                // Previene entradas excesivamente largas o abusivas.
        }
    }
}