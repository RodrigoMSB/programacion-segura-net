using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeguridadBancoFinal.Services;
using SeguridadBancoFinal.Models;
using SeguridadBancoFinal.DTOs;

namespace SeguridadBancoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITransferenciaService _transferenciaService;

        public AdminController(IUsuarioService usuarioService, ITransferenciaService transferenciaService)
        {
            _usuarioService = usuarioService;
            _transferenciaService = transferenciaService;
        }

        // ================================
        // GET: /admin/usuarios
        // ================================
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

        // ================================
        // GET: /admin/movimientos
        // ================================
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

        // ================================
        // POST: /admin/crear-cuenta
        // ================================
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