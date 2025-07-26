// ============================================================================
// AuditoriaTransferencia.cs — Entidad de Auditoría para Transferencias
// ============================================================================
// Esta clase representa un **registro detallado** de cada transferencia bancaria,
// con fines de **auditoría, trazabilidad y monitoreo de seguridad**.
//
// A diferencia de la entidad `Movimiento`, que refleja la lógica operativa,
// `AuditoriaTransferencia` guarda también **información contextual del usuario**,
// como su email e IP de origen, sin acoplarse a la lógica financiera directa.
//
// -----------------------------------------------------------------------------
// CONCEPTOS CLAVE
// -----------------------------------------------------------------------------
// - Persistencia independiente: no está ligada a relaciones navegables.
// - No contiene claves foráneas estrictas por decisión de diseño:
//   Se prioriza la trazabilidad histórica sobre la integridad referencial.
// - Datos sensibles como IP y correo permiten rastrear incidentes de seguridad.
//
// ============================================================================

namespace SeguridadBancoFinal.Models
{
    /// <summary>
    /// Representa una entrada de auditoría generada automáticamente
    /// al realizar una transferencia bancaria entre cuentas.
    /// 
    /// Su propósito es **documentar evidencia completa** de la operación:
    /// - Origen y destino de fondos.
    /// - Montos y descripción.
    /// - Usuario que originó la acción (vía email).
    /// - IP desde la cual se ejecutó.
    /// 
    /// Esta información es crítica para:
    /// - Cumplimiento normativo (compliance).
    /// - Trazabilidad de incidentes o fraudes.
    /// - Revisiones administrativas posteriores.
    /// </summary>
    public class AuditoriaTransferencia
    {
        /// <summary>
        /// Identificador único de la auditoría.
        /// Clave primaria de la tabla.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID de la cuenta desde donde se originó la transferencia.
        /// No es clave foránea: se guarda como valor histórico.
        /// </summary>
        public int CuentaOrigenId { get; set; }

        /// <summary>
        /// ID de la cuenta de destino de la transferencia.
        /// No es clave foránea: se guarda como valor histórico.
        /// </summary>
        public int CuentaDestinoId { get; set; }

        /// <summary>
        /// Monto transferido, expresado en decimal de alta precisión.
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// Descripción opcional de la transferencia.
        /// Puede ser un mensaje personalizado introducido por el usuario.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha y hora (UTC) en la que se registró la auditoría.
        /// Suele coincidir con el momento de la transferencia.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Correo electrónico del usuario autenticado que realizó la acción.
        /// Permite identificar al actor humano del evento.
        /// </summary>
        public string UsuarioEmail { get; set; }

        /// <summary>
        /// Dirección IP del cliente desde la cual se originó la transferencia.
        /// Clave para detectar patrones sospechosos o intrusiones.
        /// </summary>
        public string IP { get; set; }
    }
}