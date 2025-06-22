// ================================================================
//   AdminController.cs — Controlador protegido (Versión SEGURA)
// ================================================================
// Este controlador expone un recurso restringido que solo puede ser
// accedido por usuarios autenticados mediante JWT y con el rol "Admin".
//
// Forma parte del flujo seguro del Ejemplo SEGURO — Capítulo 5,
// demostrando cómo aplicar control de acceso robusto usando
// autenticación y autorización basadas en roles.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Proporciona el atributo [Authorize] y tipos relacionados para
// controlar acceso a controladores y acciones.
using Microsoft.AspNetCore.Authorization;

// Proporciona clases base y atributos para construir controladores API.
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo5.Controllers
{
    /// <summary>
    /// Controlador que expone recursos protegidos para administradores.
    /// 
    /// Se asegura de que solo usuarios autenticados con JWT válido
    /// y con el rol "Admin" puedan acceder a las rutas expuestas.
    /// </summary>
    [ApiController] // Activa características automáticas para APIs REST.
    [Route("[controller]")] // Ruta base: /admin
    [Authorize(Roles = "Admin")] // Restringe acceso a usuarios con rol Admin.
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP GET que representa un recurso sensible
        /// accesible solo por usuarios autenticados con rol "Admin".
        /// 
        /// Este recurso sirve para demostrar cómo el middleware
        /// de autorización de ASP.NET Core verifica el JWT,
        /// extrae los claims y aplica la restricción de rol.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK si el token es válido y el usuario tiene rol adecuado;
        /// de lo contrario, HTTP 401 o 403 gestionados automáticamente
        /// por el middleware de autorización.
        /// </returns>
        [HttpGet("recurso")]
        public IActionResult RecursoProtegido()
        {
            return Ok("Acceso concedido al recurso de administrador con JWT válido y rol Admin.");
        }
    }
}
