// ================================================================
//   AccountApiController.cs — Controlador REST (Versión SEGURA)
// ================================================================
// Provee endpoints JSON para:
// - Registrar un usuario con hash + salt usando CryptoService.
// - Autenticar al usuario validando el hash almacenado.
// - Consultar perfil del usuario autenticado.
// Usa Session para simular persistencia de login entre peticiones.
// ================================================================

// ----------------------------------------------------------------
// Importaciones requeridas:
// ----------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;               // Para atributos de controlador y IActionResult.
using Microsoft.AspNetCore.Http;              // Para trabajar con Session (SetString/GetString).
using EjemploSeguroCapitulo9.Models;          // Para usar la entidad Usuario.
using EjemploSeguroCapitulo9.Services;        // Para inyectar CryptoService.
using System.Collections.Generic;             // Para List<T>.
using System.Linq;                            // Para FirstOrDefault().

// ----------------------------------------------------------------
// Declaración del espacio de nombres
// ----------------------------------------------------------------
namespace EjemploSeguroCapitulo9.Controllers
{
    /// <summary>
    /// API REST principal para registro, login y perfil de usuario seguro.
    /// </summary>
    [ApiController]
    [Route("api/account")]
    public class AccountApiController : ControllerBase
    {
        // ----------------------------------------------------------------
        // Dependencia para funciones criptográficas.
        // ----------------------------------------------------------------
        private readonly CryptoService _crypto;

        // ----------------------------------------------------------------
        // Simula una base de datos en memoria para almacenar usuarios.
        // ⚠️ Solo para propósitos educativos y pruebas con SAST/DAST.
        // ----------------------------------------------------------------
        private static List<Usuario> _usuarios = new();

        /// <summary>
        /// Inyección de dependencias (CryptoService).
        /// </summary>
        public AccountApiController(CryptoService crypto)
        {
            _crypto = crypto;
        }

        // ----------------------------------------------------------------
        // POST: api/account/register
        //
        // Registra un nuevo usuario aplicando hash + salt.
        // La contraseña nunca se almacena en texto plano.
        // ----------------------------------------------------------------
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario input)
        {
            var (hash, salt) = _crypto.HashPassword(input.Password);

            _usuarios.Add(new Usuario
            {
                Nombre = input.Nombre,
                Password = $"{hash}:{salt}" // Guardar hash y salt juntos.
            });

            return Ok(new { message = "Usuario registrado de forma segura (API REST)." });
        }

        // ----------------------------------------------------------------
        // POST: api/account/login
        //
        // Verifica las credenciales:
        // - Busca el usuario.
        // - Valida el hash con CryptoService.
        // - Crea sesión en memoria.
        // ----------------------------------------------------------------
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario input)
        {
            var user = _usuarios.FirstOrDefault(u => u.Nombre == input.Nombre);
            if (user == null)
                return Unauthorized(new { error = "Usuario no existe." });

            var parts = user.Password.Split(':');
            if (parts.Length != 2)
                return Unauthorized(new { error = "Formato de credencial inválido." });

            string hashStored = parts[0];
            string saltStored = parts[1];

            bool ok = _crypto.VerifyPassword(input.Password, saltStored, hashStored);
            if (!ok)
                return Unauthorized(new { error = "Contraseña incorrecta." });

            // Guarda el nombre de usuario en Session.
            HttpContext.Session.SetString("ApiUsername", user.Nombre);

            return Ok(new { message = $"Login OK para {user.Nombre}" });
        }

        // ----------------------------------------------------------------
        // GET: api/account/profile
        //
        // Retorna el perfil del usuario actualmente logueado.
        // No expone información sensible.
        // ----------------------------------------------------------------
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("ApiUsername");
            if (string.IsNullOrEmpty(username))
                return Unauthorized(new { error = "No autenticado o sesión expirada." });

            return Ok(new
            {
                username = username,
                note = "Este perfil NO expone la contraseña. (API REST segura)"
            });
        }
    }
}