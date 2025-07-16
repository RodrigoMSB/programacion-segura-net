using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    public class CrearClienteRequest
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}