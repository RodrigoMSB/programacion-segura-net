// ================================================================
// TransferenciaService.cs — Servicio de Transferencias y Auditoría
// ================================================================
// Este servicio encapsula toda la lógica de negocio relacionada con:
// - Transferencias entre cuentas bancarias.
// - Validaciones de integridad y reglas de negocio.
// - Registro de movimientos contables.
// - Auditoría de operaciones sensibles (IP y usuario).
//
// Cumple los principios de:
// - Separación de responsabilidades.
// - Trazabilidad y transparencia de operaciones.
// - Seguridad mediante validaciones previas.
//
// ================================================================

using SeguridadBancoFinal.Data;
using SeguridadBancoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Servicio encargado de orquestar las transferencias entre cuentas
    /// y mantener la trazabilidad mediante registros de auditoría.
    /// </summary>
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">DbContext de EF Core configurado.</param>
        public TransferenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================================
        // MÉTODO: Transferir
        // ==========================================================

        /// <summary>
        /// Ejecuta una transferencia entre dos cuentas bancarias:
        /// - Valida que ambas cuentas existan y que el saldo sea suficiente.
        /// - Descuenta e incrementa saldos.
        /// - Registra el movimiento.
        /// - Deja trazabilidad en la tabla de auditoría.
        /// </summary>
        /// <param name="cuentaOrigenId">ID de la cuenta que envía dinero.</param>
        /// <param name="cuentaDestinoId">ID de la cuenta que recibe dinero.</param>
        /// <param name="monto">Monto a transferir (debe ser > 0).</param>
        /// <param name="descripcion">Descripción opcional del movimiento.</param>
        /// <param name="emailUsuario">Email del usuario autenticado que ejecuta.</param>
        /// <param name="ipOrigen">Dirección IP del cliente (para auditoría).</param>
        /// <exception cref="ArgumentException">Si el monto no es válido o se transfiere a sí mismo.</exception>
        /// <exception cref="InvalidOperationException">Si las cuentas no existen o saldo es insuficiente.</exception>
        public void Transferir(int cuentaOrigenId, int cuentaDestinoId, decimal monto, string descripcion, string emailUsuario, string ipOrigen)
        {
            // Validaciones previas
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            if (cuentaOrigenId == cuentaDestinoId)
                throw new ArgumentException("No se puede transferir a la misma cuenta.");

            // Cargar ambas cuentas desde base de datos
            var cuentaOrigen = _context.CuentasBancarias
                                       .FirstOrDefault(c => c.Id == cuentaOrigenId)
                                ?? throw new InvalidOperationException("Cuenta origen no encontrada.");

            var cuentaDestino = _context.CuentasBancarias
                                        .FirstOrDefault(c => c.Id == cuentaDestinoId)
                                ?? throw new InvalidOperationException("Cuenta destino no encontrada.");

            // Validar fondos suficientes
            if (cuentaOrigen.Saldo < monto)
                throw new InvalidOperationException("Saldo insuficiente en la cuenta origen.");

            // Ajustar saldos en memoria (no persistido aún)
            cuentaOrigen.Saldo -= monto;
            cuentaDestino.Saldo += monto;

            // Crear objeto Movimiento (registro contable)
            var movimiento = new Movimiento
            {
                CuentaOrigenId = cuentaOrigenId,
                CuentaDestinoId = cuentaDestinoId,
                Monto = monto,
                Descripcion = descripcion,
                Fecha = DateTime.UtcNow
            };

            _context.Movimientos.Add(movimiento);

            // Crear registro de auditoría con metadatos
            var auditoria = new AuditoriaTransferencia
            {
                CuentaOrigenId = cuentaOrigenId,
                CuentaDestinoId = cuentaDestinoId,
                Monto = monto,
                Descripcion = descripcion,
                Fecha = DateTime.UtcNow,
                UsuarioEmail = emailUsuario,
                IP = ipOrigen
            };

            _context.AuditoriasTransferencias.Add(auditoria);

            // Persistir todas las operaciones en una única transacción
            _context.SaveChanges();
        }

        // ==========================================================
        // MÉTODO: ObtenerMovimientosPorCuenta
        // ==========================================================

        /// <summary>
        /// Retorna todos los movimientos en los que una cuenta fue origen o destino.
        /// Ideal para mostrar a un usuario sus transacciones personales.
        /// </summary>
        /// <param name="cuentaId">ID de la cuenta bancaria.</param>
        /// <returns>Listado ordenado de movimientos.</returns>
        public IEnumerable<Movimiento> ObtenerMovimientosPorCuenta(int cuentaId)
        {
            return _context.Movimientos
                           .Include(m => m.CuentaOrigen)
                           .Include(m => m.CuentaDestino)
                           .Where(m => m.CuentaOrigenId == cuentaId || m.CuentaDestinoId == cuentaId)
                           .OrderByDescending(m => m.Fecha)
                           .AsNoTracking()
                           .ToList();
        }

        // ==========================================================
        // MÉTODO: ObtenerTodosLosMovimientos
        // ==========================================================

        /// <summary>
        /// Retorna todos los movimientos del sistema.
        /// Solo debe usarse en contextos administrativos.
        /// </summary>
        /// <returns>Listado completo de movimientos.</returns>
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