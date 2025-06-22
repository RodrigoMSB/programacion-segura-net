// ================================================================
//   UsuarioController.cs — Controlador (Versión SEGURA)
// ================================================================
// Este archivo define el controlador principal para la gestión de
// usuarios en la versión segura del Capítulo 3. Implementa:
// - Validación de entrada mediante ModelState y atributos de datos.
// - Control de acceso basado en roles mediante headers.
// - Separación de responsabilidades delegando la lógica de negocio
//   a la capa de servicio.
// - Manejo seguro de secretos con restricción de acceso.
// ================================================================

using EjemploSeguroCapitulo3.Models;
using EjemploSeguroCapitulo3.Services;
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo3.Controllers
{
    /// <summary>
    /// Controlador para orquestar operaciones de usuarios.
    /// Aplica validación de modelo, control de roles y
    /// separación de lógica de negocio mediante un servicio dedicado.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Servicio encargado de gestionar usuarios y secretos.
        /// </summary>
        private readonly UsuarioService _servicio;

        /// <summary>
        /// Constructor que inyecta la dependencia del servicio de usuarios.
        /// </summary>
        /// <param name="servicio">Instancia de UsuarioService.</param>
        public UsuarioController(UsuarioService servicio)
        {
            _servicio = servicio;
        }

        /// <summary>
        /// Endpoint para registrar un nuevo usuario.
        /// Valida los datos de entrada según el modelo definido.
        /// </summary>
        /// <param name="usuario">Datos del usuario enviados en el cuerpo de la solicitud.</param>
        /// <returns>Confirmación de creación o errores de validación.</returns>
        [HttpPost("crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _servicio.CrearUsuario(usuario);
            return Ok("Usuario creado de forma segura.");
        }

        /// <summary>
        /// Endpoint para obtener la lista de todos los usuarios registrados.
        /// Requiere un rol válido ('Admin' o 'User') especificado en el header.
        /// </summary>
        /// <param name="rol">Rol del solicitante, enviado como header HTTP.</param>
        /// <returns>Lista de usuarios o error de autorización.</returns>
        [HttpGet("todos")]
        public IActionResult ObtenerTodos([FromHeader] string rol)
        {
            if (rol != "Admin" && rol != "User")
                return Unauthorized("Rol no autorizado.");

            return Ok(_servicio.ObtenerUsuarios());
        }

        /// <summary>
        /// Endpoint para obtener el secreto protegido de la API.
        /// Solo disponible para usuarios con rol 'Admin'.
        /// </summary>
        /// <param name="rol">Rol del solicitante, debe ser 'Admin'.</param>
        /// <returns>Secreto si autorizado, o error de acceso.</returns>
        [HttpGet("secreto")]
        public IActionResult ObtenerSecreto([FromHeader] string rol)
        {
            try
            {
                var secreto = _servicio.ObtenerApiKey(rol);
                return Ok($"Secreto protegido: {secreto}");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}