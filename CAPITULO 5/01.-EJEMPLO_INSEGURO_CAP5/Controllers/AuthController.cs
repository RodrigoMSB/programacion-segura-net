// ================================================================
//   AuthController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este controlador simula un flujo de autenticación y autorización
// de forma totalmente insegura.
//
// Principales problemas expuestos:
// - No valida credenciales ni aplica políticas de autenticación reales.
// - No emite JWT ni utiliza cookies seguras.
// - Permite que el cliente defina su rol (por ejemplo: 'Admin').
// - Rutas críticas accesibles sin control de identidad ni permisos.
//
// Este diseño se usa para contrastar con la versión segura
// donde se aplican tokens firmados y control de acceso.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Importa el modelo de dominio que representa al usuario.
using EjemploInseguroCapitulo5.Models;

// Importa componentes para construir controladores API en ASP.NET Core.
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo5.Controllers
{
    /// <summary>
    /// Controlador que simula un flujo de autenticación básica.
    /// En este ejemplo inseguro, se omite todo mecanismo de seguridad real.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Almacena los usuarios simulados en memoria.
        /// Esta lista persiste mientras dure la instancia de la aplicación.
        /// </summary>
        private static List<Usuario> _usuarios = new List<Usuario>();

        /// <summary>
        /// Endpoint HTTP POST para simular un login.
        /// 
        /// Problemas intencionales:
        /// - No verifica si el usuario existe.
        /// - No comprueba la contraseña.
        /// - Permite que el cliente envíe su propio rol, incluso 'Admin'.
        /// - No emite JWT ni cookies de sesión seguras.
        /// 
        /// Retorna un mensaje con los datos enviados tal cual.
        /// </summary>
        /// <param name="usuario">Objeto Usuario enviado por el cliente.</param>
        /// <returns>HTTP 200 OK con mensaje de bienvenida y rol.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            // No hay validación ni hashing de credenciales.
            // Cualquier rol que el cliente envíe es aceptado.
            _usuarios.Add(usuario);

            return Ok($"Bienvenido {usuario.Nombre} con rol {usuario.Rol}. No se emite JWT.");
        }

        /// <summary>
        /// Endpoint HTTP GET que simula un recurso restringido
        /// para administradores. En esta versión insegura,
        /// no hay verificación de token, sesión ni rol real.
        /// Permite acceso libre a cualquier solicitante.
        /// </summary>
        /// <returns>HTTP 200 OK con mensaje de acceso permitido.</returns>
        [HttpGet("recurso-admin")]
        public IActionResult RecursoAdmin()
        {
            // No se revisa ningún token ni cabecera de autenticación.
            // Se otorga acceso incondicional.
            return Ok("Acceso concedido a recurso de administrador SIN autenticación real.");
        }
    }
}