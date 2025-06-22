// ================================================================
// UsuarioController.cs — Controlador de usuario
// ================================================================
// Define los endpoints principales para registrar, autenticar 
// y listar usuarios en la API.
// Aplica principios de seguridad y validación de datos.
// ================================================================

// -----------------------------------------------------------------
// IMPORTS
// -----------------------------------------------------------------

// Librería base de ASP.NET Core para crear controladores web API.
using Microsoft.AspNetCore.Mvc;

// Importa el modelo de dominio Usuario.
using EjemploSeguridadCapitulo1.Models;

// Importa el servicio de autenticación para hash y validación de contraseñas.
using EjemploSeguridadCapitulo1.Services;

// Importa el contexto de datos para operaciones CRUD con EF Core.
using EjemploSeguridadCapitulo1.Data;

namespace EjemploSeguridadCapitulo1.Controllers
{
    /// <summary>
    /// Controlador responsable de manejar operaciones relacionadas con usuarios.
    /// Expone endpoints seguros para:
    /// - Registro de usuarios con hash de contraseña
    /// - Autenticación básica (verificación de credenciales)
    /// - Listado de usuarios registrados
    /// 
    /// Principios aplicados:
    /// - Validación de datos de entrada.
    /// - Pensamiento defensivo contra entradas maliciosas.
    /// - Separación de responsabilidades entre controlador y servicio.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Contexto de base de datos inyectado para acceder a entidades persistidas.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Servicio de autenticación inyectado para aplicar hashing y verificación de contraseñas.
        /// </summary>
        private readonly AutenticacionService _authService;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// Permite resolver el contexto de datos y el servicio de autenticación.
        /// </summary>
        /// <param name="context">Instancia del ApplicationDbContext.</param>
        /// <param name="authService">Instancia del servicio de autenticación.</param>
        public UsuarioController(ApplicationDbContext context, AutenticacionService authService)
        {
            _context = context;
            _authService = authService;
        }

        /// <summary>
        /// Endpoint para registrar un nuevo usuario.
        /// Valida los datos de entrada, verifica duplicados y aplica hash a la contraseña antes de guardar.
        /// </summary>
        /// <param name="nuevoUsuario">Datos del nuevo usuario.</param>
        /// <returns>Resultado HTTP indicando éxito o error.</returns>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario nuevoUsuario)
        {
            // Validar que el modelo cumpla con las anotaciones de datos.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar duplicados por nombre de usuario.
            if (_context.Usuarios.Any(u => u.Username == nuevoUsuario.Username))
                return Conflict("El nombre de usuario ya está en uso.");

            // Hashear la contraseña antes de persistir.
            nuevoUsuario.PasswordHash = _authService.HashearPassword(nuevoUsuario.PasswordHash);

            // Guardar en base de datos.
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return Ok("Usuario registrado correctamente.");
        }

        /// <summary>
        /// Endpoint de login. Verifica si las credenciales ingresadas son correctas.
        /// No genera tokens JWT; solo devuelve resultado de autenticación.
        /// </summary>
        /// <param name="login">Credenciales de acceso.</param>
        /// <returns>Resultado HTTP de éxito o error de autenticación.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            // Validar entrada básica.
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("Debe ingresar usuario y contraseña.");

            // Buscar usuario por nombre.
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Username == login.Username);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado.");

            // Validar contraseña usando el servicio de autenticación.
            if (!_authService.VerificarPassword(login.Password, usuario.PasswordHash))
                return Unauthorized("Contraseña incorrecta.");

            return Ok($"Autenticación correcta para el usuario: {usuario.Username}");
        }

        /// <summary>
        /// Endpoint para listar todos los usuarios registrados.
        /// Nota: En un entorno real, este endpoint debería estar protegido por roles o políticas de autorización.
        /// </summary>
        /// <returns>Lista de usuarios con datos básicos (sin exponer hash de contraseñas).</returns>
        [HttpGet("listar")]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _context.Usuarios
                .Select(u => new { u.Id, u.Username, u.Email, u.Rol })
                .ToList();

            return Ok(usuarios);
        }

        /// <summary>
        /// Clase auxiliar para recibir las credenciales de inicio de sesión.
        /// Se usa como DTO (Data Transfer Object).
        /// </summary>
        public class LoginRequest
        {
            /// <summary>
            /// Nombre de usuario ingresado.
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Contraseña en texto plano que será validada.
            /// </summary>
            public string Password { get; set; }
        }
    }
}