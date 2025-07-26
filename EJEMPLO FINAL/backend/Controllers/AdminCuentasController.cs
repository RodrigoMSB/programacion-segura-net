// ===================================================
// IMPORTACIONES DE DEPENDENCIAS NECESARIAS
// ===================================================

using Microsoft.AspNetCore.Authorization;         // Habilita el uso de políticas de autorización por roles
using Microsoft.AspNetCore.Mvc;                   // Proporciona herramientas para crear controladores API
using SeguridadBancoFinal.DTOs;                   // Contiene los DTOs usados para transferencia de datos
using SeguridadBancoFinal.Models;                 // Contiene las entidades del dominio (Modelo)
using SeguridadBancoFinal.Data;                   // Permite acceder al contexto de base de datos (DbContext)

// ===================================================
// CONTROLADOR ADMINISTRATIVO DE CUENTAS BANCARIAS
// ===================================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador exclusivo para usuarios con rol "Admin".
    /// Permite consultar y crear cuentas bancarias de forma centralizada.
    /// </summary>
    [ApiController]
    [Route("admin/cuentas")]
    [Authorize(Roles = "Admin")] // Solo accesible para usuarios autenticados con rol de Administrador
    public class AdminCuentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos por inyección de dependencias.
        /// </summary>
        public AdminCuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================================================
        // GET: /admin/cuentas
        // ===================================================

        /// <summary>
        /// Retorna una lista de todas las cuentas bancarias registradas en el sistema.
        /// Incluye ID, número de cuenta, saldo y referencia al ID de usuario.
        /// </summary>
        /// <returns>Lista de cuentas en formato anónimo</returns>
        [HttpGet]
        public IActionResult GetTodasLasCuentas()
        {
            var cuentas = _context.CuentasBancarias
                                  .Select(c => new
                                  {
                                      c.Id,
                                      c.NumeroCuenta,
                                      c.Saldo,
                                      c.UsuarioId
                                  })
                                  .ToList();

            return Ok(cuentas);
        }

        // ===================================================
        // POST: /admin/cuentas
        // ===================================================

        /// <summary>
        /// Crea una nueva cuenta bancaria asociada a un usuario existente.
        /// Solo se permite si el número de cuenta no está duplicado.
        /// </summary>
        /// <param name="dto">Objeto DTO con datos de cuenta: número, saldo e ID del usuario</param>
        /// <returns>Mensaje de éxito o error según validaciones</returns>
        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CuentaBancariaDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            // Validación: el usuario debe existir
            var usuario = _context.Usuarios.Find(dto.UsuarioId);
            if (usuario == null)
                return BadRequest("El usuario especificado no existe.");

            // Validación: el número de cuenta debe ser único
            bool numeroExiste = _context.CuentasBancarias.Any(c => c.NumeroCuenta == dto.NumeroCuenta);
            if (numeroExiste)
                return BadRequest("El número de cuenta ya existe.");

            // Crear la cuenta si pasa validaciones
            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = dto.NumeroCuenta,
                Saldo = dto.Saldo,
                UsuarioId = dto.UsuarioId
            };

            _context.CuentasBancarias.Add(cuenta);
            _context.SaveChanges();

            return Ok(new { message = "Cuenta creada correctamente." });
        }
    }
}