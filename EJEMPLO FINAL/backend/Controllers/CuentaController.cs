// =============================================
// IMPORTACIONES DE DEPENDENCIAS Y SERVICIOS
// =============================================

using Microsoft.AspNetCore.Authorization; // Habilita autorización basada en políticas o roles para los endpoints
using Microsoft.AspNetCore.Mvc; // Proporciona herramientas para construir controladores y manejar solicitudes HTTP
using SeguridadBancoFinal.Services; // Referencia los servicios que encapsulan la lógica de negocio
using System.Security.Claims; // Permite acceder a los claims del usuario autenticado (ej: email)

// =============================================
// CONTROLADOR: CUENTA
// =============================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las cuentas bancarias del usuario autenticado.
    /// Permite obtener saldos y listar las cuentas propias.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize] // Requiere autenticación JWT válida para acceder a cualquier acción de este controlador
    public class CuentaController : ControllerBase
    {
        // =============================================
        // DEPENDENCIAS INYECTADAS
        // =============================================

        private readonly IUsuarioService _usuarioService;
        private readonly ICuentaService _cuentaService;

        /// <summary>
        /// Constructor que recibe los servicios necesarios vía inyección de dependencias.
        /// </summary>
        /// <param name="usuarioService">Servicio para acceder a datos del usuario autenticado.</param>
        /// <param name="cuentaService">Servicio que permite acceder a las cuentas bancarias del usuario.</param>
        public CuentaController(IUsuarioService usuarioService, ICuentaService cuentaService)
        {
            _usuarioService = usuarioService;
            _cuentaService = cuentaService;
        }

        // ============================================================
        // GET: /cuenta/saldo
        // ============================================================

        /// <summary>
        /// Obtiene el saldo de todas las cuentas asociadas al usuario autenticado.
        /// </summary>
        /// <returns>Lista de objetos con número de cuenta y saldo.</returns>
        [HttpGet("saldo")]
        public IActionResult ObtenerSaldo()
        {
            // Obtener email del usuario desde el JWT
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Unauthorized(); // Token no contiene información válida

            var usuario = _usuarioService.ObtenerPorEmail(email);

            // Validar existencia del usuario y de cuentas asociadas
            if (usuario == null || usuario.Cuentas.Count == 0)
                return NotFound("Cuenta no encontrada.");

            // Seleccionar número y saldo de cada cuenta asociada
            var cuentas = usuario.Cuentas
                .Select(c => new { c.NumeroCuenta, c.Saldo })
                .ToList();

            return Ok(cuentas);
        }

        // ============================================================
        // GET: /cuenta/mis-cuentas
        // ============================================================

        /// <summary>
        /// Retorna todas las cuentas bancarias del usuario autenticado,
        /// incluyendo el identificador único (ID), número de cuenta y saldo actual.
        /// Este endpoint es utilizado por el frontend para mostrar la lista de cuentas.
        /// </summary>
        /// <returns>Listado de cuentas del usuario actual</returns>
        [HttpGet("mis-cuentas")]
        public IActionResult ObtenerMisCuentas()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var usuario = _usuarioService.ObtenerPorEmail(email);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            var cuentas = _cuentaService.ObtenerCuentasPorUsuario(usuario.Id);

            var resultado = cuentas.Select(c => new
            {
                id = c.Id, // Este campo es clave para futuras operaciones (por ejemplo, transferencias)
                numeroCuenta = c.NumeroCuenta,
                saldo = c.Saldo
            }).ToList();

            return Ok(resultado);
        }
    }
}