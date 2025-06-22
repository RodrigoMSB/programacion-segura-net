// ================================================================
//   SessionController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este controlador demuestra prácticas incorrectas en el manejo
// de sesiones. Fue diseñado intencionalmente con configuraciones
// débiles para contrastar con la versión segura del Capítulo 7.
//
// Vulnerabilidades ilustradas:
//  - Cookies de sesión sin protección.
//  - Sesión sin expiración adecuada.
//  - Logout incompleto: no invalida cookie en cliente.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

// Proporciona atributos y clases base para construir controladores REST.
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo7.Controllers
{
    /// <summary>
    /// Controlador responsable de simular autenticación,
    /// acceso a perfil y cierre de sesión, con malas prácticas.
    /// Ruta base: /session
    /// </summary>
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP POST que simula inicio de sesión.
        /// No verifica credenciales reales ni aplica regeneración de ID de sesión.
        /// Guarda el nombre en la sesión sin cifrado ni validación.
        /// </summary>
        /// <param name="username">Nombre del usuario a registrar en la sesión.</param>
        /// <returns>Mensaje de confirmación de inicio de sesión inseguro.</returns>
        [HttpPost("login")]
        public IActionResult Login(string username)
        {
            // Almacena el nombre directamente en sesión
            HttpContext.Session.SetString("Usuario", username);

            return Ok($"Sesión INSEGURA iniciada para {username}.");
        }

        /// <summary>
        /// Endpoint HTTP GET que simula acceso a un perfil de usuario.
        /// Retorna información si la sesión existe, de lo contrario deniega acceso.
        /// Muestra cómo la sesión nunca expira ni protege adecuadamente.
        /// </summary>
        /// <returns>Datos del perfil o error si no hay sesión válida.</returns>
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            // Obtiene el nombre de usuario almacenado en sesión
            var usuario = HttpContext.Session.GetString("Usuario");

            if (usuario == null)
                return Unauthorized("Sesión inválida o expirada.");

            return Ok($"Perfil inseguro de {usuario}. La sesión nunca expira ni se protege.");
        }

        /// <summary>
        /// Endpoint HTTP POST que simula cierre de sesión.
        /// Utiliza Session.Clear() pero no revoca ni borra la cookie en cliente.
        /// Permite que la cookie pueda ser reutilizada.
        /// </summary>
        /// <returns>Mensaje indicando cierre de sesión incompleto.</returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Limpia valores en servidor pero no fuerza invalidación de cookie
            HttpContext.Session.Clear();

            return Ok("Logout inseguro: la cookie de sesión sigue viva.");
        }
    }
}