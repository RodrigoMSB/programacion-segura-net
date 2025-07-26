// ===================================================
// IMPORTACIONES NECESARIAS PARA EL CONTROLADOR ADMIN
// ===================================================

using Microsoft.AspNetCore.Authorization;          // Permite aplicar políticas de autorización, como roles
using Microsoft.AspNetCore.Mvc;                    // Contiene herramientas para definir controladores y acciones HTTP
using SeguridadBancoFinal.Services;                // Interfaces de servicios de lógica de negocio (usuario, cuenta, transferencia)
using SeguridadBancoFinal.Models;                  // Entidades de la base de datos (como CuentaBancaria)
using SeguridadBancoFinal.DTOs;                    // Objetos de transferencia de datos usados para entrada/salida en API

// ===================================================
// CONTROLADOR DE ADMINISTRACIÓN GENERAL
// ===================================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador exclusivo para administradores.
    /// Permite la gestión de usuarios, movimientos y cuentas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")] // Solo usuarios con rol Admin pueden acceder
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITransferenciaService _transferenciaService;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        public AdminController(IUsuarioService usuarioService, ITransferenciaService transferenciaService)
        {
            _usuarioService = usuarioService;
            _transferenciaService = transferenciaService;
        }

        // ===================================================
        // GET: /admin/usuarios
        // ===================================================

        /// <summary>
        /// Retorna una lista de todos los usuarios registrados en el sistema.
        /// Incluye solo los campos clave: Id, Nombre, Email y Rol.
        /// </summary>
        [HttpGet("usuarios")]
        public IActionResult ObtenerUsuarios()
        {
            var usuarios = _usuarioService.ObtenerTodos()
                .Select(u => new
                {
                    u.Id,
                    u.Nombre,
                    u.Email,
                    u.Rol
                });

            return Ok(usuarios);
        }

        // ===================================================
        // GET: /admin/movimientos
        // ===================================================

        /// <summary>
        /// Devuelve el historial completo de movimientos registrados en el sistema.
        /// Visible solo para administradores.
        /// </summary>
        [HttpGet("movimientos")]
        public IActionResult ObtenerTodosLosMovimientos()
        {
            var movimientos = _transferenciaService.ObtenerTodosLosMovimientos()
                .Select(m => new
                {
                    m.Id,
                    m.Monto,
                    m.Fecha,
                    m.Descripcion,
                    Origen = m.CuentaOrigen.NumeroCuenta,
                    Destino = m.CuentaDestino.NumeroCuenta
                });

            return Ok(movimientos);
        }

        // ===================================================
        // POST: /admin/crear-cuenta
        // ===================================================

        /// <summary>
        /// Crea una nueva cuenta bancaria y la asigna a un usuario existente.
        /// </summary>
        /// <param name="cuentaDto">DTO con información de cuenta: número, saldo y usuarioId</param>
        /// <param name="cuentaService">Servicio de cuentas inyectado desde el contenedor</param>
        [HttpPost("crear-cuenta")]
        public IActionResult CrearCuenta([FromBody] CuentaBancariaDTO cuentaDto, [FromServices] ICuentaService cuentaService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = cuentaDto.NumeroCuenta,
                Saldo = cuentaDto.Saldo,
                UsuarioId = cuentaDto.UsuarioId
            };

            cuentaService.CrearCuenta(cuenta);

            return Ok("Cuenta creada exitosamente y asignada al usuario.");
        }
    }
}