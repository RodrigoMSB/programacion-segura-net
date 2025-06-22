// ================================================================
//   AccountController.cs — Controlador (Versión INSEGURA, Corregido)
// ================================================================
// Controlador MVC deliberadamente inseguro para el capítulo 9:
// - Permite registro y login sin validación de credenciales ni hash.
// - Guarda usuario y contraseña en texto plano usando TempData.
// - Expone datos sensibles en la vista de perfil.
// - Usa TempData.Keep() para persistir información en múltiples requests.
//
// ❌ Prohibido usar este patrón en producción: es solo para SAST/DAST.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc; // Base para controladores MVC y vistas.
using EjemploInseguroCapitulo9.Models; // Entidad de dominio Usuario.

namespace EjemploInseguroCapitulo9.Controllers
{
    /// <summary>
    /// Controlador MVC inseguro:
    /// - No aplica validación ni protección CSRF.
    /// - Simula un flujo vulnerable para prácticas de análisis.
    /// </summary>
    [Route("[controller]")] // Ruta base: /Account
    public class AccountController : Controller // ✅ Es Controller (no ControllerBase)
    {
        // -----------------------------------------------------------
        // GET: /Account/Register
        //
        // Muestra la vista de formulario de registro inseguro.
        // -----------------------------------------------------------
        [HttpGet("Register")]
        public IActionResult Register() => View();

        // -----------------------------------------------------------
        // POST: /Account/Register
        //
        // Procesa los datos enviados desde el formulario de registro.
        // ❌ No realiza validación ni hashing.
        // Guarda valores en TempData en texto plano.
        // Redirige al perfil.
        // -----------------------------------------------------------
        [HttpPost("Register")]
        public IActionResult Register(Usuario usuario)
        {
            TempData["Username"] = usuario.Nombre;
            TempData["Password"] = usuario.Password;
            return RedirectToAction("Profile");
        }

        // -----------------------------------------------------------
        // GET: /Account/Login
        //
        // Muestra la vista de formulario de inicio de sesión.
        // -----------------------------------------------------------
        [HttpGet("Login")]
        public IActionResult Login() => View();

        // -----------------------------------------------------------
        // POST: /Account/Login
        //
        // Procesa los datos enviados desde el formulario de login.
        // ❌ No verifica credenciales contra base de datos.
        // Guarda valores en TempData sin cifrado.
        // Redirige al perfil.
        // -----------------------------------------------------------
        [HttpPost("Login")]
        public IActionResult Login(Usuario usuario)
        {
            TempData["Username"] = usuario.Nombre;
            TempData["Password"] = usuario.Password;
            return RedirectToAction("Profile");
        }

        // -----------------------------------------------------------
        // GET: /Account/Profile
        //
        // Renderiza la vista de perfil inseguro.
        // Muestra nombre de usuario y contraseña en texto claro.
        // Usa TempData.Keep() para persistir valores.
        // -----------------------------------------------------------
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            ViewBag.Username = TempData.Peek("Username") ?? "Invitado";
            ViewBag.Password = TempData.Peek("Password") ?? "N/A";
            return View();
        }
    }
}