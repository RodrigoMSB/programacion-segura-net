// ============================================================================
// CrearClienteRequest.cs — DTO para registro de clientes (por Admin)
// ============================================================================
// Este DTO define la estructura mínima necesaria para crear un nuevo cliente
// desde el panel de administración.
//
// Se utiliza en el endpoint:
//
//     - POST /admin/usuarios
//
// No incluye contraseña ni rol, ya que se presupone que la creación automática
// de credenciales o la asignación por defecto es responsabilidad del backend.
//
// ----------------------------------------------------------------------------
// CONTEXTO DE USO
// ----------------------------------------------------------------------------
// - Permite registrar clientes de forma rápida sin intervención del usuario.
// - Utilizado en escenarios donde el administrador crea cuentas por lote
//   o en la inscripción inicial de clientes.
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) utilizado para registrar
    /// nuevos clientes desde el módulo administrativo.
    /// 
    /// Contiene solo el nombre y el email del cliente.
    /// La generación de credenciales y rol es delegada al sistema.
    /// </summary>
    public class CrearClienteRequest
    {
        /// <summary>
        /// Nombre completo del cliente a registrar.
        /// Este campo es obligatorio.
        /// </summary>
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Correo electrónico del cliente.
        /// Validado automáticamente como email válido.
        /// Este campo es obligatorio.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}