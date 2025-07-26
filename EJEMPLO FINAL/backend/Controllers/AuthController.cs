// =============================================
// IMPORTACIONES DE DEPENDENCIAS Y SERVICIOS
// =============================================

using Microsoft.AspNetCore.Mvc; // Proporciona las herramientas para definir controladores y manejar peticiones HTTP
using SeguridadBancoFinal.DTOs; // Importa los modelos DTO usados para la comunicación entre cliente y servidor
using SeguridadBancoFinal.Services; // Importa los servicios de lógica de negocio (usuarios, autenticación, etc.)

// =============================================
// CONTROLADOR: AUTHENTICACIÓN Y REGISTRO
// =============================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador responsable del inicio de sesión, registro de usuarios
    /// y creación de cuentas de clientes. Expone endpoints públicos sin autorización previa.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // =============================================
        // DEPENDENCIAS INYECTADAS
        // =============================================

        private readonly IUsuarioService _usuarioService;
        private readonly AuthService _authService;

        /// <summary>
        /// Constructor que recibe los servicios necesarios por inyección de dependencias.
        /// </summary>
        public AuthController(IUsuarioService usuarioService, AuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        // =============================================
        // POST: /auth/login
        // =============================================

        /// <summary>
        /// Autentica al usuario usando email y contraseña.
        /// Retorna un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="request">DTO con email y password del usuario.</param>
        /// <returns>Token JWT o mensaje de error.</returns>
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

        // =============================================
        // POST: /auth/register
        // (Registro de nuevos usuarios)
        // =============================================

        /// <summary>
        /// Registra un nuevo usuario con nombre, email y contraseña.
        /// Puede ser utilizado por administradores para crear cuentas.
        /// </summary>
        /// <param name="request">DTO con los datos del nuevo usuario.</param>
        /// <returns>Mensaje de éxito o error.</returns>
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

        // =============================================
        // POST: /auth/crear-cliente
        // Solo para administradores
        // =============================================

        /// <summary>
        /// Crea un nuevo cliente sin contraseña asignada.
        /// Pensado para uso administrativo, por ejemplo para preinscribir usuarios.
        /// </summary>
        /// <param name="request">DTO con nombre y email del nuevo cliente.</param>
        /// <returns>Mensaje indicando éxito o error.</returns>
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

        // =============================================
        // POST: /auth/completar-registro
        // =============================================

        /// <summary>
        /// Permite a un cliente previamente creado completar su registro
        /// estableciendo una contraseña.
        /// </summary>
        /// <param name="request">DTO con email y nueva contraseña.</param>
        /// <returns>Mensaje indicando resultado del proceso.</returns>
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