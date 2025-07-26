// ================================================================================================
// LoginRequestValidator.cs — Validación del DTO de Inicio de Sesión (Login)
// ================================================================================================
// Este validador aplica reglas de integridad sobre los datos que el usuario envía
// al intentar iniciar sesión. Utiliza la librería FluentValidation para centralizar
// y simplificar la validación del DTO LoginRequest.
//
// Se valida:
// - Que el campo Email no esté vacío y tenga un formato válido.
// - Que la contraseña tenga un mínimo de 6 caracteres.
//
// Al utilizar FluentValidation:
// - Se desacopla la lógica de validación del controlador.
// - Se mejora la mantenibilidad y testeo.
// - Se facilita la localización de errores con mensajes específicos.
//
// --------------------------------------------------------------------------------
// IMPORTS — Referencias necesarias para validación y acceso al DTO a validar
// --------------------------------------------------------------------------------
using FluentValidation;                       // Librería principal para validaciones expresivas.
using SeguridadBancoFinal.DTOs;               // Acceso al DTO LoginRequest.

// --------------------------------------------------------------------------------
// ESPACIO DE NOMBRES
// --------------------------------------------------------------------------------
namespace SeguridadBancoFinal.Validation
{
    /// <summary>
    /// Validador especializado para el DTO LoginRequest.
    /// Contiene las reglas que deben cumplirse antes de procesar un inicio de sesión.
    /// Se activa automáticamente cuando se utiliza FluentValidation en ASP.NET.
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// Constructor donde se definen todas las reglas de validación para los campos del login.
        /// </summary>
        public LoginRequestValidator()
        {
            // ==========================================================================
            // VALIDACIÓN: Email
            // ==========================================================================
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")               // No puede estar vacío.
                .EmailAddress().WithMessage("Formato de email inválido.");        // Debe tener formato válido (xxx@yyy.zzz).

            // ==========================================================================
            // VALIDACIÓN: Password
            // ==========================================================================
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")          // No puede estar vacía.
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres."); // Longitud mínima recomendada.
        }
    }
}