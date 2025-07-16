using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    public interface ICuentaService
    {
        void CrearCuenta(CuentaBancaria cuenta);
        List<CuentaBancaria> ObtenerCuentasPorUsuario(int usuarioId);
    }
}