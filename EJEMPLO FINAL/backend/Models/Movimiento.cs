using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeguridadBancoFinal.Models
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CuentaOrigenId { get; set; }

        [ForeignKey("CuentaOrigenId")]
        public CuentaBancaria CuentaOrigen { get; set; }

        [Required]
        public int CuentaDestinoId { get; set; }

        [ForeignKey("CuentaDestinoId")]
        public CuentaBancaria CuentaDestino { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
    }
}