using SeguridadBancoFinal.Data;
using SeguridadBancoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace SeguridadBancoFinal.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ApplicationDbContext _context;

        public TransferenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===========================================
        // Realizar transferencia entre cuentas
        // ===========================================
        public void Transferir(int cuentaOrigenId, int cuentaDestinoId, decimal monto, string descripcion)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            if (cuentaOrigenId == cuentaDestinoId)
                throw new ArgumentException("No se puede transferir a la misma cuenta.");

            // Obtener cuentas con control de concurrencia
            var cuentaOrigen = _context.CuentasBancarias
                                       .FirstOrDefault(c => c.Id == cuentaOrigenId)
                                ?? throw new InvalidOperationException("Cuenta origen no encontrada.");

            var cuentaDestino = _context.CuentasBancarias
                                        .FirstOrDefault(c => c.Id == cuentaDestinoId)
                                ?? throw new InvalidOperationException("Cuenta destino no encontrada.");

            if (cuentaOrigen.Saldo < monto)
                throw new InvalidOperationException("Saldo insuficiente en la cuenta origen.");

            // Actualizar saldos
            cuentaOrigen.Saldo -= monto;
            cuentaDestino.Saldo += monto;

            // Registrar movimiento
            var movimiento = new Movimiento
            {
                CuentaOrigenId = cuentaOrigenId,
                CuentaDestinoId = cuentaDestinoId,
                Monto = monto,
                Descripcion = descripcion,
                Fecha = DateTime.UtcNow
            };

            _context.Movimientos.Add(movimiento);
            _context.SaveChanges();
        }

        // ===========================================
        // Obtener movimientos de una cuenta específica
        // ===========================================
        public IEnumerable<Movimiento> ObtenerMovimientosPorCuenta(int cuentaId)
        {
            return _context.Movimientos
                           .Where(m => m.CuentaOrigenId == cuentaId || m.CuentaDestinoId == cuentaId)
                           .OrderByDescending(m => m.Fecha)
                           .AsNoTracking()
                           .ToList();
        }

        // ===========================================
        // Obtener todos los movimientos (Admin)
        // ===========================================
        public IEnumerable<Movimiento> ObtenerTodosLosMovimientos()
        {
            return _context.Movimientos
                           .Include(m => m.CuentaOrigen)
                           .Include(m => m.CuentaDestino)
                           .OrderByDescending(m => m.Fecha)
                           .AsNoTracking()
                           .ToList();
        }
    }
}