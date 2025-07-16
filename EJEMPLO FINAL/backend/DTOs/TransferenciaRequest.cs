using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    public class TransferenciaRequest
    {
        [Required]
        public int CuentaOrigenId { get; set; }

        [Required]
        public int CuentaDestinoId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
    }
}