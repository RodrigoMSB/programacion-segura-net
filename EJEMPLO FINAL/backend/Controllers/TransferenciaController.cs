// =====================================
// Importaciones de dependencias externas y servicios
// =====================================

using Microsoft.AspNetCore.Authorization; // Requiere autenticación y autorización para el acceso a los endpoints
using Microsoft.AspNetCore.Mvc; // Proporciona clases base para construir controladores HTTP en ASP.NET Core
using SeguridadBancoFinal.DTOs; // Contiene los Data Transfer Objects utilizados para la comunicación entre cliente y servidor
using SeguridadBancoFinal.Services; // Contiene las interfaces y servicios que encapsulan la lógica de negocio
using System.Security.Claims; // Permite acceder a las identidades y claims del usuario autenticado (JWT)

// =====================================
// Espacio de nombres del controlador
// =====================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador encargado de manejar las operaciones relacionadas con transferencias de dinero
    /// entre cuentas bancarias, incluyendo el registro y la consulta de movimientos.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize] // Requiere autenticación JWT para todos los endpoints del controlador
    public class TransferenciaController : ControllerBase
    {
        // =============================
        // Dependencias inyectadas
        // =============================
        private readonly ITransferenciaService _transferenciaService;
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Constructor principal del controlador, con inyección de dependencias.
        /// </summary>
        /// <param name="transferenciaService">Servicio de lógica de negocio para transferencias bancarias.</param>
        /// <param name="usuarioService">Servicio que proporciona acceso a datos del usuario autenticado.</param>
        public TransferenciaController(ITransferenciaService transferenciaService, IUsuarioService usuarioService)
        {
            _transferenciaService = transferenciaService;
            _usuarioService = usuarioService;
        }

        // ================================================================
        // POST: /transferencia/enviar
        // ================================================================

        /// <summary>
        /// Realiza una transferencia de dinero entre dos cuentas bancarias.
        /// El usuario autenticado debe ser dueño de la cuenta origen.
        /// </summary>
        /// <param name="request">Objeto que contiene los datos requeridos para la transferencia.</param>
        /// <returns>Resultado de la operación: éxito, error de validación o problema de autorización.</returns>
        [HttpPost("enviar")]
        public IActionResult Transferir([FromBody] TransferenciaRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos.");

            // Extraer email desde el JWT
            var email = User.FindFirstValue(ClaimTypes.Email);
            var usuario = _usuarioService.ObtenerPorEmail(email);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado.");

            // Validar que la cuenta de origen pertenezca al usuario autenticado
            var cuentaOrigen = usuario.Cuentas.FirstOrDefault(c => c.Id == request.CuentaOrigenId);
            if (cuentaOrigen == null)
                return Forbid("No puedes transferir desde una cuenta que no te pertenece.");

            if (cuentaOrigen.Saldo < request.Monto)
                return BadRequest("Fondos insuficientes en la cuenta origen.");

            try
            {
                // Obtener la dirección IP del cliente para la auditoría
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP desconocida";

                _transferenciaService.Transferir(
                    request.CuentaOrigenId,
                    request.CuentaDestinoId,
                    request.Monto,
                    request.Descripcion,
                    email,
                    ip
                );

                return Ok("Transferencia realizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ================================================================
        // GET: /transferencia/mis
        // ================================================================

        /// <summary>
        /// Obtiene todos los movimientos asociados a las cuentas del usuario autenticado.
        /// </summary>
        /// <returns>Lista de movimientos ordenados por fecha descendente.</returns>
        [HttpGet("mis")]
        public IActionResult ObtenerMisMovimientos()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email no encontrado en el token.");

            var usuario = _usuarioService.ObtenerPorEmail(email);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            try
            {
                var movimientos = usuario.Cuentas
                    .SelectMany(c => _transferenciaService.ObtenerMovimientosPorCuenta(c.Id))
                    .OrderByDescending(m => m.Fecha)
                    .Select(m => new
                    {
                        m.Id,
                        m.Monto,
                        m.Fecha,
                        m.Descripcion,
                        Origen = m.CuentaOrigen?.NumeroCuenta ?? "(Cuenta origen desconocida)",
                        Destino = m.CuentaDestino?.NumeroCuenta ?? "(Cuenta destino desconocida)"
                    })
                    .ToList();

                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar movimientos: {ex.Message}");
            }
        }

        // ================================================================
        // GET: /transferencia/por-cuenta/{numeroCuenta}
        // ================================================================

        /// <summary>
        /// Obtiene los movimientos bancarios asociados a un número de cuenta específico.
        /// </summary>
        /// <param name="numeroCuenta">Número de cuenta a consultar.</param>
        /// <returns>Lista de movimientos ordenados por fecha descendente.</returns>
        [HttpGet("por-cuenta/{numeroCuenta}")]
        public IActionResult ObtenerMovimientosPorNumeroCuenta(string numeroCuenta)
        {
            if (string.IsNullOrEmpty(numeroCuenta))
                return BadRequest("Número de cuenta no proporcionado.");

            var cuenta = _usuarioService.ObtenerCuentaPorNumero(numeroCuenta);

            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");

            try
            {
                var movimientos = _transferenciaService.ObtenerMovimientosPorCuenta(cuenta.Id)
                    .OrderByDescending(m => m.Fecha)
                    .Select(m => new
                    {
                        m.Id,
                        m.Monto,
                        m.Fecha,
                        m.Descripcion,
                        Origen = m.CuentaOrigen?.NumeroCuenta ?? "(Cuenta origen desconocida)",
                        Destino = m.CuentaDestino?.NumeroCuenta ?? "(Cuenta destino desconocida)"
                    })
                    .ToList();

                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener movimientos: {ex.Message}");
            }
        }
    }
}