// ============================================================================
// RegisterRequest.cs — DTO para Registro de Nuevos Usuarios
// ============================================================================
// Este DTO encapsula los datos necesarios para registrar un nuevo usuario
// dentro del sistema bancario seguro.
//
// Se utiliza típicamente en métodos [HttpPost] del controlador de autenticación,
// específicamente en el endpoint POST /auth/register.
//
// ----------------------------------------------------------------------------
// FUNCIONALIDAD CLAVE
// ----------------------------------------------------------------------------
// - Valida que el nombre, email y contraseña sean proporcionados.
// - Asegura que el email tenga un formato válido.
// - Enforce mínimos de seguridad (ej: longitud de contraseña).
// - Soporta la creación de usuarios con roles personalizados (por defecto "Cliente").
// ----------------------------------------------------------------------------
// Esta clase es esencial para mantener la integridad y seguridad de los datos
// durante el proceso de creación de cuentas.
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado para encapsular
    /// la información requerida en el proceso de registro de un nuevo usuario.
    /// 
    /// Este DTO será consumido por el controlador de autenticación al momento
    /// de crear un nuevo registro de usuario en la base de datos.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Nombre completo del usuario. Campo obligatorio.
        /// 
        /// Reglas de validación:
        /// - No puede estar vacío.
        /// - Longitud máxima: 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario. Campo obligatorio.
        /// 
        /// Reglas de validación:
        /// - Debe tener un formato de email válido.
        /// - Longitud máxima: 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña para el acceso del usuario. Campo obligatorio.
        /// 
        /// Reglas de validación:
        /// - Mínimo 6 caracteres para seguridad básica.
        /// - Se recomienda aplicar hashing en la capa de servicio.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario en el sistema (opcional).
        /// Por defecto se asigna el rol "Cliente".
        /// 
        /// Puede ser sobrescrito en procesos administrativos.
        /// </summary>
        [StringLength(20)]
        public string? Rol { get; set; } = "Cliente";
    }
}