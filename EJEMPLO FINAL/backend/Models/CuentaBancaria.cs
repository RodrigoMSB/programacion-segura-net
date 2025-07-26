// ============================================================================
// CuentaBancaria.cs — Entidad de Dominio: Cuenta Bancaria de Usuario
// ============================================================================
// Esta clase representa una **cuenta bancaria individual** perteneciente a un
// usuario del sistema. Es el núcleo de la lógica de movimientos y transferencias,
// ya que cada cuenta puede actuar como origen o destino en múltiples operaciones.
//
// -----------------------------------------------------------------------------
// CONCEPTOS CLAVE
// -----------------------------------------------------------------------------
// - ForeignKey explícita: `UsuarioId` enlaza esta cuenta con su propietario.
// - Relación inversa: El usuario tiene una colección de cuentas.
// - Navegación doble: La cuenta se conecta con movimientos tanto como origen
//   como destino (relación uno-a-muchos en dos direcciones).
// - Persistencia decimal: Asegura precisión en operaciones monetarias.
//
// ============================================================================

// -----------------------------------------------------------------------------
// IMPORTS — Librerías esenciales para validaciones y relaciones ORM.
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;        // Reglas de validación.
using System.ComponentModel.DataAnnotations.Schema; // Relaciones y precisión de columnas.

namespace SeguridadBancoFinal.Models
{
    /// <summary>
    /// Entidad que representa una cuenta bancaria asociada a un usuario del sistema.
    /// Cada cuenta mantiene su saldo actual y puede participar en múltiples movimientos,
    /// tanto como cuenta de origen como cuenta de destino.
    /// </summary>
    public class CuentaBancaria
    {
        /// <summary>
        /// Identificador único de la cuenta.
        /// Clave primaria utilizada para relaciones internas.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Número identificador visible de la cuenta bancaria.
        /// Se limita a 20 caracteres como máximo.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string NumeroCuenta { get; set; } = string.Empty;

        /// <summary>
        /// Saldo actual de la cuenta.
        /// Se almacena con precisión decimal (18,2) para evitar errores de redondeo.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; } = 0;

        // -------------------------------------------------------------------------
        // RELACIÓN CON USUARIO
        // -------------------------------------------------------------------------

        /// <summary>
        /// Clave foránea que enlaza esta cuenta con un usuario.
        /// Establece la relación de propiedad.
        /// </summary>
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Propiedad de navegación hacia el usuario propietario de esta cuenta.
        /// Permite acceder directamente al dueño desde la cuenta.
        /// </summary>
        public Usuario Usuario { get; set; }

        // -------------------------------------------------------------------------
        // RELACIÓN CON MOVIMIENTOS
        // -------------------------------------------------------------------------

        /// <summary>
        /// Colección de movimientos en los que esta cuenta actuó como origen.
        /// Útil para trazabilidad de salidas de dinero.
        /// </summary>
        public ICollection<Movimiento> MovimientosOrigen { get; set; } = new List<Movimiento>();

        /// <summary>
        /// Colección de movimientos en los que esta cuenta fue destino.
        /// Útil para trazabilidad de ingresos recibidos.
        /// </summary>
        public ICollection<Movimiento> MovimientosDestino { get; set; } = new List<Movimiento>();
    }
}