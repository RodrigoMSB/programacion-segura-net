// ==========================================================================
// ICuentaService.cs — Contrato del Servicio de Gestión de Cuentas Bancarias
// ==========================================================================
// Esta interfaz define el contrato para las operaciones relacionadas
// con la administración de cuentas bancarias dentro del sistema.
//
// Funcionalidades clave:
// - Creación de cuentas bancarias vinculadas a usuarios existentes.
// - Obtención de todas las cuentas asociadas a un usuario.
//
// Justificación arquitectónica:
// - Promueve el principio de separación de responsabilidades (SRP).
// - Permite la inversión de dependencias (DIP) para facilitar pruebas y modularidad.
// - Mantiene la lógica de negocio encapsulada en una capa de servicios.
//
// ==========================================================================

using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Define las operaciones que deben implementarse para la gestión
    /// de cuentas bancarias en el sistema.
    /// </summary>
    public interface ICuentaService
    {
        /// <summary>
        /// Crea una nueva cuenta bancaria.
        /// Debe validarse externamente que el usuario asociado exista y que
        /// el número de cuenta sea único antes de invocar esta operación.
        /// </summary>
        /// <param name="cuenta">Instancia de la cuenta bancaria con datos iniciales.</param>
        void CrearCuenta(CuentaBancaria cuenta);

        /// <summary>
        /// Obtiene todas las cuentas bancarias registradas para un usuario específico.
        /// Permite al usuario autenticado (o al administrador) visualizar su portafolio.
        /// </summary>
        /// <param name="usuarioId">Identificador único del usuario.</param>
        /// <returns>Lista de cuentas bancarias asociadas al usuario.</returns>
        List<CuentaBancaria> ObtenerCuentasPorUsuario(int usuarioId);
    }
}