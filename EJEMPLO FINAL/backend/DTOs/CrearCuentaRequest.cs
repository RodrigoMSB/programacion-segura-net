namespace SeguridadBancoFinal.DTOs
{
    public class CrearCuentaRequest
    {
        public int UsuarioId { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; } = 0;
    }
}