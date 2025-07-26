// ============================================================================
// TransferenciaRequest.cs — DTO para Solicitudes de Transferencias Bancarias
// ============================================================================
// Este DTO encapsula los datos que se requieren desde el cliente (frontend)
// para ejecutar una transferencia entre dos cuentas bancarias.
//
// Se utiliza típicamente en métodos [HttpPost] de los controladores de API
// y permite una validación automática de entrada gracias a los atributos
// de anotación de datos (Data Annotations).
//
// ----------------------------------------------------------------------------
// FUNCIONALIDAD CLAVE
// ----------------------------------------------------------------------------
// - Valida que los IDs de cuenta sean provistos.
// - Asegura que el monto sea mayor que cero.
// - Permite una descripción opcional y controlada.
// ----------------------------------------------------------------------------
// Se complementa con la lógica de negocio contenida en TransferenciaService.
//
// Uso común en: POST /transferencia/enviar
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos utilizado para encapsular la solicitud
    /// de una operación de transferencia entre dos cuentas bancarias.
    /// </summary>
    public class TransferenciaRequest
    {
        /// <summary>
        /// ID de la cuenta de origen desde donde se descontará el monto.
        /// Este campo es obligatorio y debe referenciar una cuenta válida
        /// que pertenezca al usuario autenticado.
        /// </summary>
        [Required]
        public int CuentaOrigenId { get; set; }

        /// <summary>
        /// ID de la cuenta de destino a la cual se transferirá el monto.
        /// Este campo también es obligatorio y debe existir en el sistema.
        /// </summary>
        [Required]
        public int CuentaDestinoId { get; set; }

        /// <summary>
        /// Monto a transferir, expresado en unidades monetarias.
        /// Debe ser un valor positivo mayor a 0.01. Se valida automáticamente.
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        /// <summary>
        /// Campo opcional que permite ingresar un mensaje o motivo de la transferencia.
        /// Se limita a un máximo de 200 caracteres.
        /// </summary>
        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
    }
}