// ============================================================================
// CompletarRegistroRequest.cs — DTO para completar el registro de cliente
// ============================================================================
// Este DTO permite a un usuario completar su registro asignando su contraseña,
// partiendo desde una cuenta que fue previamente creada por el administrador.
//
// ----------------------------------------------------------------------------
// CONTEXTO DE USO
// ----------------------------------------------------------------------------
// - Usado en flujos donde el cliente es pre-registrado por un administrador
//   (por ejemplo, en una empresa o institución).
// - Posteriormente, el cliente accede al sistema con su email y establece
//   su contraseña personal mediante este objeto.
// - Este mecanismo mejora la seguridad y experiencia inicial.
//
// Endpoint asociado (por ejemplo):
//     POST /auth/completar-registro
// ============================================================================

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// DTO utilizado para completar el proceso de registro de un usuario que ya fue
    /// creado en el sistema. Este objeto contiene el email previamente registrado
    /// y la nueva contraseña que el usuario desea establecer.
    /// </summary>
    public class CompletarRegistroRequest
    {
        /// <summary>
        /// Email con el que el usuario fue pre-registrado.
        /// Debe coincidir con un usuario existente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nueva contraseña definida por el usuario para completar su registro.
        /// Debe cumplir las políticas de seguridad del sistema (longitud mínima, etc.).
        /// </summary>
        public string Password { get; set; }
    }
}