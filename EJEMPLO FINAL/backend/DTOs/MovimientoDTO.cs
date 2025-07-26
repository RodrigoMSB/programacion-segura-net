// ============================================================================
// MovimientoDTO.cs — DTO para visualización de movimientos bancarios
// ============================================================================
// Esta clase define un Data Transfer Object (DTO) que representa una
// versión simplificada y segura de un movimiento bancario. Su propósito es
// exponer solo los datos necesarios a las vistas del frontend, sin revelar
// referencias complejas a entidades del modelo o detalles internos.
//
// Este DTO es ideal para ser usado en endpoints de consulta como:
// GET /cliente/movimientos
// GET /admin/movimientos
//
// ----------------------------------------------------------------------------
// CONTEXTO
// ----------------------------------------------------------------------------
// En lugar de retornar toda la entidad `Movimiento` que contiene referencias
// cruzadas a `CuentaBancaria`, este DTO centraliza solo la información que
// se necesita para mostrar al usuario:
// - Identificadores
// - Monto y descripción
// - Fechas legibles
// - Información de origen/destino formateada
// ============================================================================

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que representa los datos clave
    /// de un movimiento de transferencia entre cuentas.
    /// 
    /// Se utiliza para desacoplar la lógica interna del modelo de base
    /// de datos de la respuesta enviada al cliente.
    /// </summary>
    public class MovimientoDTO
    {
        /// <summary>
        /// Identificador único del movimiento.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Monto involucrado en la transferencia.
        /// Representado en formato decimal para manejar valores monetarios.
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// Fecha y hora en que se realizó el movimiento.
        /// Se espera que se muestre en formato local en el frontend.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Descripción opcional del movimiento.
        /// Útil para observaciones del usuario o sistema.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Representación del número o nombre de la cuenta de origen.
        /// Este valor es generado en el servicio a partir del modelo completo.
        /// </summary>
        public string Origen { get; set; } = string.Empty;

        /// <summary>
        /// Representación del número o nombre de la cuenta de destino.
        /// Este valor también es generado en la capa de servicio.
        /// </summary>
        public string Destino { get; set; } = string.Empty;
    }
}