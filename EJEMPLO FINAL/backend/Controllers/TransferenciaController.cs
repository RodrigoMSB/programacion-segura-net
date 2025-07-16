using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeguridadBancoFinal.DTOs;
using SeguridadBancoFinal.Services;
using System.Security.Claims;

namespace SeguridadBancoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;
        private readonly IUsuarioService _usuarioService;

        public TransferenciaController(ITransferenciaService transferenciaService, IUsuarioService usuarioService)
        {
            _transferenciaService = transferenciaService;
            _usuarioService = usuarioService;
        }

        // ================================
        // POST: /transferencia/enviar
        // ================================
        [HttpPost("enviar")]
        public IActionResult Transferir([FromBody] TransferenciaRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos.");

            try
            {
                _transferenciaService.Transferir(
                    request.CuentaOrigenId,
                    request.CuentaDestinoId,
                    request.Monto,
                    request.Descripcion
                );

                return Ok("Transferencia realizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================================
        // GET: /transferencia/mis
        // ================================
        [HttpGet("mis")]
        public IActionResult ObtenerMisMovimientos()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var usuario = _usuarioService.ObtenerPorEmail(email);

            if (usuario == null)
                return Unauthorized();

            var movimientos = usuario.Cuentas
                .SelectMany(c => _transferenciaService.ObtenerMovimientosPorCuenta(c.Id))
                .OrderByDescending(m => m.Fecha)
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
    }
}