// ================================================================================================
// RegisterRequestValidator.cs — Validador para Registro de Usuarios
// ================================================================================================
// Este validador asegura que los datos enviados durante el proceso de registro cumplan
// con los requisitos mínimos de integridad, formato y seguridad.
//
// Se valida:
// - Nombre: requerido y con máximo de 50 caracteres.
// - Email: requerido, formato válido y máximo de 100 caracteres.
// - Contraseña: reglas estrictas de complejidad para fortalecer la seguridad.
// - Rol: debe ser explícitamente "Cliente" o "Admin" (o null por defecto).
//
// Utiliza FluentValidation para centralizar la lógica de validación, promover
// la reutilización y desacoplar las reglas de los controladores.
//
// --------------------------------------------------------------------------------
// IMPORTS — Referencias necesarias para las reglas de validación y DTO asociado
// --------------------------------------------------------------------------------
using FluentValidation;                       // Framework de validación extensible y declarativo.
using SeguridadBancoFinal.DTOs;               // Acceso al DTO RegisterRequest.

// --------------------------------------------------------------------------------
// ESPACIO DE NOMBRES
// --------------------------------------------------------------------------------
namespace SeguridadBancoFinal.Validation
{
    /// <summary>
    /// Validador específico para el DTO RegisterRequest.
    /// Aplica validaciones sobre nombre, email, contraseña y rol durante el registro.
    /// </summary>
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        /// <summary>
        /// Define las reglas de validación aplicables al registro de un nuevo usuario.
        /// </summary>
        public RegisterRequestValidator()
        {
            // ==========================================================================
            // VALIDACIÓN: Nombre
            // ==========================================================================
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")                 // No debe estar vacío.
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres."); // Límite razonable para nombres.

            // ==========================================================================
            // VALIDACIÓN: Email
            // ==========================================================================
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")                  // Campo requerido.
                .EmailAddress().WithMessage("Formato de email inválido.")            // Validación de sintaxis.
                .MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres.");

            // ==========================================================================
            // VALIDACIÓN: Password
            // ==========================================================================
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")             // Campo requerido.
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.") // Complejidad básica.
                .Matches("[A-Z]").WithMessage("Debe incluir al menos una letra mayúscula.")      // Seguridad adicional.
                .Matches("[a-z]").WithMessage("Debe incluir al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("Debe incluir al menos un número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Debe incluir al menos un símbolo.");        // Requiere carácter especial.

            // ==========================================================================
            // VALIDACIÓN: Rol
            // ==========================================================================
            RuleFor(x => x.Rol)
                .Must(r => r == "Cliente" || r == "Admin" || r == null)              // Solo valores permitidos.
                .WithMessage("El rol debe ser Cliente o Admin.");                    // Impide inyecciones de roles arbitrarios.
        }
    }
}