using Microsoft.AspNetCore.Mvc;
using SeguridadBancoFinal.DTOs;
using SeguridadBancoFinal.Services;

namespace SeguridadBancoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly AuthService _authService;

        public AuthController(IUsuarioService usuarioService, AuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        // ================================
        // POST: /auth/login
        // ================================
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest("Cuerpo de solicitud vacío.");

            var usuario = _usuarioService.ValidarCredenciales(request.Email, request.Password);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            var token = _authService.GenerateToken(usuario);
            return Ok(new { token });
        }

        // ================================
        // POST: /auth/register
        // (opcional - para admins)
        // ================================
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (request == null)
                return BadRequest("Cuerpo de solicitud vacío.");

            try
            {
                var nuevoUsuario = _usuarioService.RegistrarUsuario(
                    request.Nombre,
                    request.Email,
                    request.Password,
                    request.Rol ?? "Cliente"
                );

                return Ok(new { message = $"Usuario {nuevoUsuario.Email} registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================================
        // POST: /auth/crear-cliente
        // Solo para Admin, crea cliente sin password
        // ================================
        [HttpPost("crear-cliente")]
        public IActionResult CrearCliente([FromBody] CrearClienteRequest request)
        {
            if (request == null)
                return BadRequest("Cuerpo de solicitud vacío.");

            try
            {
                var nuevoUsuario = _usuarioService.CrearClienteSinPassword(
                    request.Nombre,
                    request.Email
                );

                return Ok(new { message = $"Cliente {nuevoUsuario.Email} creado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("completar-registro")]
        public IActionResult CompletarRegistro([FromBody] CompletarRegistroRequest request)
        {
            if (request == null)
                return BadRequest("Cuerpo de solicitud vacío.");

            try
            {
                _usuarioService.CompletarRegistroCliente(
                    request.Email,
                    request.Password
                );

                return Ok(new { message = $"Cliente {request.Email} completó su registro correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}