// ============================================================================
// CuentaBancariaDTO.cs — DTO para creación de cuentas bancarias
// ============================================================================
// Esta clase define la estructura de datos utilizada para enviar o recibir
// información relacionada con la creación o visualización de cuentas bancarias
// en el sistema.
//
// Se emplea en acciones como:
//
//     - POST /admin/cuentas         (crear nueva cuenta)
//     - GET  /admin/cuentas         (listar cuentas)
//
// Este DTO encapsula los datos esenciales sin exponer relaciones complejas
// ni referencias circulares, siguiendo el principio de separación de modelos.
//
// ----------------------------------------------------------------------------
// NOTAS DE DISEÑO
// ----------------------------------------------------------------------------
// - Se omite el campo `Id` ya que se espera que lo gestione el backend.
// - El DTO está orientado a la vista administrativa para creación de cuentas.
// ============================================================================

namespace SeguridadBancoFinal.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que representa una cuenta bancaria
    /// de forma simplificada para operaciones de creación o consulta básica.
    /// 
    /// Utilizado principalmente por administradores del sistema.
    /// </summary>
    public class CuentaBancariaDTO
    {
        /// <summary>
        /// Número único de la cuenta bancaria.
        /// Debe cumplir validaciones de unicidad y formato en capa de servicio.
        /// </summary>
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Monto actual o inicial de saldo en la cuenta bancaria.
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Identificador del usuario al cual pertenece esta cuenta.
        /// Referencia a la entidad Usuario.
        /// </summary>
        public int UsuarioId { get; set; }
    }
}