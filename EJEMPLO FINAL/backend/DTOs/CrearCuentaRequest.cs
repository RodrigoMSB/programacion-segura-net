// ============================================================================
// CrearCuentaRequest.cs — DTO para solicitud de creación de cuentas bancarias
// ============================================================================
// Este DTO representa la estructura de datos requerida para que un administrador
// del sistema cree una nueva cuenta bancaria asociada a un usuario.
//
// Se utiliza principalmente en el endpoint:
//
//     - POST /admin/cuentas
//
// Está diseñado para separar las responsabilidades de la capa de presentación
// y evitar exponer directamente la entidad `CuentaBancaria`.
//
// ----------------------------------------------------------------------------
// NOTAS DE DISEÑO
// ----------------------------------------------------------------------------
// - Contiene los datos mínimos necesarios para registrar una cuenta.
// - El campo `SaldoInicial` puede validarse en la capa de servicio para
//   asegurar que sea positivo o cero.
// ============================================================================

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) que encapsula los datos requeridos
    /// para crear una cuenta bancaria desde la capa de presentación.
    /// 
    /// Utilizado en solicitudes administrativas.
    /// </summary>
    public class CrearCuentaRequest
    {
        /// <summary>
        /// Identificador del usuario al cual se asignará la cuenta bancaria.
        /// Este ID debe corresponder a un usuario válido existente en el sistema.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Número único de cuenta bancaria a ser registrado.
        /// La unicidad debe ser validada en la capa de lógica de negocio.
        /// </summary>
        public string NumeroCuenta { get; set; } = string.Empty;

        /// <summary>
        /// Monto inicial con el que se crea la cuenta bancaria.
        /// Puede ser 0 si no se requiere depósito inicial.
        /// </summary>
        public decimal SaldoInicial { get; set; } = 0;
    }
}