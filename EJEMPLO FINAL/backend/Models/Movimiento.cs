// ===========================================================================
// Movimiento.cs — Entidad de Dominio: Movimiento Bancario entre Cuentas
// ===========================================================================
// Esta clase representa una operación financiera entre dos cuentas bancarias,
// específicamente una **transferencia** desde una cuenta origen a una cuenta destino.
//
// Es utilizada como bitácora transaccional del sistema y permite auditar el flujo
// de dinero, mostrando montos, cuentas involucradas, descripción y fecha.
//
// ---------------------------------------------------------------------------
// CONCEPTOS CLAVE
// ---------------------------------------------------------------------------
// - Transferencia: Movimiento de dinero de una cuenta a otra.
// - Relación doble (self-reference): Esta entidad se relaciona dos veces
//   con la entidad `CuentaBancaria`, como origen y como destino.
// - ForeignKey explícito: Se especifica la clave foránea con [ForeignKey]
//   para que EF reconozca las propiedades de navegación.
//
// - Persistencia decimal: El atributo [Column(TypeName = "decimal(18,2)")]
//   se utiliza para asegurar precisión y evitar errores de redondeo.
//
// ===========================================================================

// ---------------------------------------------------------------------------
// IMPORTS — Librerías de anotaciones de datos y mapeo ORM.
// ---------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;       // Validaciones como Required, Key, StringLength.
using System.ComponentModel.DataAnnotations.Schema; // Atributos para ForeignKey y Column.

namespace SeguridadBancoFinal.Models
{
    /// <summary>
    /// Entidad que representa un movimiento financiero entre dos cuentas.
    /// Usualmente corresponde a una transferencia, reflejando origen, destino,
    /// monto, fecha y una descripción opcional.
    /// </summary>
    public class Movimiento
    {
        /// <summary>
        /// Identificador único del movimiento.
        /// Actúa como clave primaria.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Clave foránea que representa la cuenta de origen del dinero.
        /// </summary>
        [Required]
        public int CuentaOrigenId { get; set; }

        /// <summary>
        /// Propiedad de navegación hacia la cuenta de origen.
        /// Permite acceso a los detalles de la cuenta desde el modelo.
        /// </summary>
        [ForeignKey("CuentaOrigenId")]
        public CuentaBancaria CuentaOrigen { get; set; }

        /// <summary>
        /// Clave foránea que representa la cuenta de destino del dinero.
        /// </summary>
        [Required]
        public int CuentaDestinoId { get; set; }

        /// <summary>
        /// Propiedad de navegación hacia la cuenta de destino.
        /// </summary>
        [ForeignKey("CuentaDestinoId")]
        public CuentaBancaria CuentaDestino { get; set; }

        /// <summary>
        /// Monto de dinero transferido.
        /// Se persiste como decimal(18,2) en la base de datos
        /// para mantener precisión monetaria.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        /// <summary>
        /// Fecha y hora exacta del movimiento.
        /// Por defecto se registra en formato UTC.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Descripción opcional asociada al movimiento.
        /// Puede contener el motivo, referencia o comentario.
        /// Longitud máxima: 200 caracteres.
        /// </summary>
        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
    }
}