using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    public interface ITransferenciaService
    {
        void Transferir(int cuentaOrigenId, int cuentaDestinoId, decimal monto, string descripcion);
        IEnumerable<Movimiento> ObtenerMovimientosPorCuenta(int cuentaId);
        IEnumerable<Movimiento> ObtenerTodosLosMovimientos();
    }
}