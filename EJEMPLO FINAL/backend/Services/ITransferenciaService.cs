// ==========================================================================
// ITransferenciaService.cs — Contrato de Servicio para Transferencias Bancarias
// ==========================================================================
// Esta interfaz define el conjunto de operaciones relacionadas con
// las transferencias de dinero y la auditoría de movimientos financieros.
// 
// Funcionalidades clave:
// - Transferencia entre cuentas con control de saldos y auditoría.
// - Obtención de movimientos por cuenta individual.
// - Consulta total de movimientos para administradores.
//
// Justificación arquitectónica:
// - Separa la lógica de dominio de la infraestructura (principio SRP).
// - Facilita la inyección de dependencias y pruebas unitarias (principio DIP).
// - Promueve un diseño limpio y extensible para reglas de negocio.
//
// ==========================================================================

using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Define las operaciones esenciales del servicio de transferencias,
    /// incluyendo validaciones, persistencia de movimientos y trazabilidad.
    /// </summary>
    public interface ITransferenciaService
    {
        /// <summary>
        /// Realiza una transferencia entre dos cuentas bancarias.
        /// Valida que el monto sea positivo, que las cuentas existan,
        /// que la cuenta origen tenga saldo suficiente y que no se transfiera
        /// a sí misma. Registra tanto el movimiento como la auditoría.
        /// </summary>
        /// <param name="cuentaOrigenId">ID de la cuenta que envía el dinero.</param>
        /// <param name="cuentaDestinoId">ID de la cuenta que recibe el dinero.</param>
        /// <param name="monto">Monto a transferir (debe ser mayor a 0).</param>
        /// <param name="descripcion">Descripción opcional del movimiento.</param>
        /// <param name="emailUsuario">Email del usuario autenticado que realiza la operación.</param>
        /// <param name="ipOrigen">Dirección IP del cliente para auditoría.</param>
        void Transferir(int cuentaOrigenId, int cuentaDestinoId, decimal monto, string descripcion, string emailUsuario, string ipOrigen);

        /// <summary>
        /// Obtiene todos los movimientos asociados a una cuenta específica,
        /// tanto como origen como destino. Incluye detalles de ambas cuentas.
        /// </summary>
        /// <param name="cuentaId">ID de la cuenta a consultar.</param>
        /// <returns>Lista de movimientos ordenados por fecha (descendente).</returns>
        IEnumerable<Movimiento> ObtenerMovimientosPorCuenta(int cuentaId);

        /// <summary>
        /// Retorna todos los movimientos financieros registrados en el sistema.
        /// Usado principalmente por administradores para fines de auditoría.
        /// </summary>
        /// <returns>Lista completa de movimientos ordenada por fecha.</returns>
        IEnumerable<Movimiento> ObtenerTodosLosMovimientos();
    }
}