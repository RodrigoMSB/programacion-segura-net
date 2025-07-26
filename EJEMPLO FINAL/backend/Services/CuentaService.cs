// ===================================================================================
// CuentaService.cs — Implementación del Servicio de Gestión de Cuentas Bancarias
// ===================================================================================
// Este servicio encapsula la lógica de negocio relacionada con la creación
// y recuperación de cuentas bancarias, actuando como puente entre la capa
// de aplicación (controllers) y la capa de persistencia (EF Core).
//
// Esta clase implementa la interfaz ICuentaService, respetando los principios
// de la arquitectura limpia y asegurando la inversión de dependencias.
//
// -----------------------------------------------------------------------------------
// FUNCIONES PRINCIPALES:
// -----------------------------------------------------------------------------------
// - CrearCuenta: Agrega una nueva cuenta bancaria asociada a un usuario.
// - ObtenerCuentasPorUsuario: Retorna todas las cuentas asociadas a un usuario.
//
// -----------------------------------------------------------------------------------
// DEPENDENCIAS:
// - ApplicationDbContext: Contexto de EF Core que gestiona el acceso a la base de datos.
//
// ===================================================================================

using SeguridadBancoFinal.Data;    // Acceso a EF Core y contexto de base de datos.
using SeguridadBancoFinal.Models; // Entidades del dominio.

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Servicio responsable de manejar operaciones relacionadas con
    /// cuentas bancarias, como su creación y consulta por usuario.
    /// </summary>
    public class CuentaService : ICuentaService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia de ApplicationDbContext (EF Core).</param>
        public CuentaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // MÉTODO: CrearCuenta
        // ============================================================

        /// <summary>
        /// Registra una nueva cuenta bancaria en la base de datos.
        /// Se asume que la validación (usuario válido, número único)
        /// ya fue realizada en capas superiores.
        /// </summary>
        /// <param name="cuenta">Entidad CuentaBancaria con datos iniciales.</param>
        public void CrearCuenta(CuentaBancaria cuenta)
        {
            _context.CuentasBancarias.Add(cuenta);
            _context.SaveChanges();
        }

        // ============================================================
        // MÉTODO: ObtenerCuentasPorUsuario
        // ============================================================

        /// <summary>
        /// Recupera todas las cuentas bancarias asociadas a un usuario.
        /// Ideal para mostrar al cliente sus productos financieros.
        /// </summary>
        /// <param name="usuarioId">ID único del usuario.</param>
        /// <returns>Lista de cuentas bancarias.</returns>
        public List<CuentaBancaria> ObtenerCuentasPorUsuario(int usuarioId)
        {
            // DEBUG opcional: útil en ambientes de desarrollo o troubleshooting.
            Console.WriteLine($"[DEBUG] Buscando cuentas para UsuarioId: {usuarioId}");

            var cuentas = _context.CuentasBancarias
                                  .Where(c => c.UsuarioId == usuarioId)
                                  .ToList();

            Console.WriteLine($"[DEBUG] Cuentas encontradas: {cuentas.Count}");

            foreach (var c in cuentas)
            {
                Console.WriteLine($"[DEBUG] Cuenta: ID={c.Id}, Nro={c.NumeroCuenta}, Saldo={c.Saldo}, UsuarioId={c.UsuarioId}");
            }

            return cuentas;
        }
    }
}