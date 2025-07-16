using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeguridadBancoFinal.Services;
using System.Security.Claims;

namespace SeguridadBancoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CuentaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public CuentaController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // ================================
        // GET: /cuenta/saldo
        // ================================
        [HttpGet("saldo")]
        public IActionResult ObtenerSaldo()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var usuario = _usuarioService.ObtenerPorEmail(email);

            if (usuario == null || usuario.Cuentas.Count == 0)
                return NotFound("Cuenta no encontrada.");

            var cuentas = usuario.Cuentas
                .Select(c => new { c.NumeroCuenta, c.Saldo })
                .ToList();

            return Ok(cuentas);
        }

        // =========================================================
        // NUEVO endpoint para el frontend
        // Ruta: /cuenta/mis-cuentas
        // =========================================================
        [HttpGet("mis-cuentas")]
        public IActionResult ObtenerMisCuentas()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var usuario = _usuarioService.ObtenerPorEmail(email);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            if (usuario.Cuentas.Count == 0)
                return Ok(new List<object>());  // Devuelve lista vacía si no tiene cuentas

            var cuentas = usuario.Cuentas
                .Select(c => new
                {
                    c.NumeroCuenta,
                    c.Saldo
                })
                .ToList();

            return Ok(cuentas);
        }
    }
}