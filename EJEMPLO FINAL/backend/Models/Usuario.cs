// ================================================================
// Usuario.cs — Entidad de Dominio: Usuario del Sistema Bancario
// ================================================================
// Esta clase representa a un usuario registrado en el sistema,
// incluyendo credenciales de acceso, rol y relaciones con cuentas.
//
// Está diseñada para ser persistida mediante Entity Framework,
// con validaciones de datos a nivel de modelo y relaciones explícitas.
//
// ---------------------------------------------------------------
// CONCEPTOS CLAVE
// ---------------------------------------------------------------
// - Data Annotations: Atributos como [Required], [StringLength], [Key]
//   que definen reglas de validación y configuración de esquema.
//
// - Clave primaria: Propiedad "Id" identificada con [Key].
//
// - Relación uno-a-muchos: Un usuario puede tener múltiples cuentas,
//   lo que se modela con una colección de "CuentaBancaria".
//
// - PasswordHash y Salt: Implementación segura para autenticación.
//
// - Rol: Determina si el usuario es Cliente o Admin.
//
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Librerías de .NET para anotaciones de datos y mapeo ORM.
// ---------------------------------------------------------------

using System.ComponentModel.DataAnnotations;     // Validaciones y restricciones de atributos (como Required, StringLength).
using System.ComponentModel.DataAnnotations.Schema; // Anotaciones adicionales para relaciones (no usadas explícitamente aquí).

namespace SeguridadBancoFinal.Models
{
    /// <summary>
    /// Entidad que representa un usuario del sistema bancario.
    /// Puede tener uno o varios roles y múltiples cuentas bancarias asociadas.
    /// Esta clase es usada tanto para autenticación como para modelar relaciones.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Clave primaria única del usuario.
        /// Se genera automáticamente por la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// Requerido. Máximo 50 caracteres.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// Requerida. Validada como email y con límite de 100 caracteres.
        /// </summary>
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hash de la contraseña del usuario.
        /// No se almacena la contraseña original.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Salt único utilizado para derivar el hash de la contraseña.
        /// Mejora la seguridad contra ataques de diccionario y rainbow tables.
        /// </summary>
        [Required]
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario: puede ser "Cliente" o "Admin".
        /// Por defecto se asigna "Cliente".
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Cliente";

        /// <summary>
        /// Relación de navegación.
        /// Lista de cuentas bancarias asociadas a este usuario.
        /// Un usuario puede tener varias cuentas.
        /// </summary>
        public ICollection<CuentaBancaria> Cuentas { get; set; } = new List<CuentaBancaria>();
    }
}