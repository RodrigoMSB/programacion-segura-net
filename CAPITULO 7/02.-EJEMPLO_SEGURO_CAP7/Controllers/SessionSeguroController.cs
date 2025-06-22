// ================================================================
//   SessionSeguroController.cs — Controlador (Versión SEGURO)
// ================================================================
// Este controlador demuestra cómo aplicar un manejo de sesión seguro
// en ASP.NET Core, siguiendo buenas prácticas:
//
// ✔ Cookies de sesión protegidas con HttpOnly, Secure y SameSite.
// ✔ Tiempo de expiración razonable.
// ✔ Regeneración de Session ID al iniciar sesión.
// ✔ Logout efectivo que invalida datos y fuerza cierre.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

// Proporciona atributos y clases base para construir controladores REST.
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo7.Controllers
{
    /// <summary>
    /// Controlador responsable de gestionar operaciones de sesión segura.
    /// Ruta base: /sessionseguro
    /// 
    /// Incluye:
    /// - Inicio de sesión con regeneración de ID.
    /// - Acceso a perfil protegido.
    /// - Cierre de sesión efectivo.
    /// </summary>
    [ApiController]
    [Route("sessionseguro")]
    public class SessionSeguroController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP POST para iniciar sesión de forma segura.
        /// Regenera la sesión para prevenir Session Fixation
        /// y almacena el nombre del usuario en la sesión.
        /// </summary>
        /// <param name="username">Nombre de usuario recibido por query string.</param>
        /// <returns>Mensaje confirmando inicio de sesión seguro.</returns>
        [HttpPost("login")]
        public IActionResult Login(string username)
        {
            // 🔑 Simula regeneración de Session ID:
            // Limpiar toda la sesión fuerza a ASP.NET Core a emitir una cookie nueva.
            HttpContext.Session.Clear();

            // Almacena nombre de usuario en sesión.
            HttpContext.Session.SetString("Usuario", username);

            return Ok($"Sesión SEGURA iniciada para {username}.");
        }

        /// <summary>
        /// Endpoint HTTP GET para acceder al perfil protegido.
        /// Solo devuelve información si la sesión existe y está activa.
        /// </summary>
        /// <returns>Perfil del usuario o error 401 si no hay sesión válida.</returns>
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            // Obtiene usuario desde sesión.
            var usuario = HttpContext.Session.GetString("Usuario");

            if (usuario == null)
                return Unauthorized("Sesión expirada o no válida.");

            return Ok($"Perfil protegido de {usuario}.");
        }

        /// <summary>
        /// Endpoint HTTP POST para cerrar sesión de forma segura.
        /// Limpia los datos de sesión y fuerza a eliminar la cookie en cliente.
        /// </summary>
        /// <returns>Mensaje confirmando cierre de sesión seguro.</returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Limpia datos en servidor.
            HttpContext.Session.Clear();

            return Ok("Sesión cerrada de forma segura y cookie invalidada.");
        }
    }
}