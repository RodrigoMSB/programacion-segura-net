// ============================================================================
// LoginRequest.cs — DTO para solicitudes de inicio de sesión
// ============================================================================
// Esta clase representa la estructura de datos requerida para autenticar
// a un usuario en el sistema. Es usada principalmente en el endpoint:
//
//     POST /auth/login
//
// Contiene las validaciones necesarias para garantizar que los campos
// esenciales se proporcionen de forma segura y consistente.
//
// ----------------------------------------------------------------------------
// CONTEXTO
// ----------------------------------------------------------------------------
// Se recomienda que este DTO se utilice únicamente en el contexto del
// controlador de autenticación. Evita exponer información adicional
// o sensible, limitándose solo a los datos necesarios para el login.
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que encapsula los datos necesarios
    /// para iniciar sesión en la aplicación.
    /// 
    /// Valida formato de email y requisitos mínimos de seguridad
    /// en la contraseña antes de procesar la solicitud.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Correo electrónico del usuario.
        /// Debe tener un formato válido y no estar vacío.
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña ingresada por el usuario.
        /// Se requiere un mínimo de 6 caracteres para cumplir requisitos de seguridad básicos.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}