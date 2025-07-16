using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeguridadBancoFinal.Models
{
    public class CuentaBancaria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; } = 0;

        // Foreign Key
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Navegación
        public Usuario Usuario { get; set; }

        public ICollection<Movimiento> MovimientosOrigen { get; set; } = new List<Movimiento>();
        public ICollection<Movimiento> MovimientosDestino { get; set; } = new List<Movimiento>();
    }
}