// ================================================================
//   AuthController.cs — Controlador (Versión SEGURO, COMPLETO)
// ================================================================
// Este controlador valida credenciales de usuario y emite un JWT
// firmado, que incluye claims de identidad y rol.
//
// Forma parte del Ejemplo SEGURO — Capítulo 5, demostrando
// cómo aplicar autenticación robusta en una API REST.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Importa el modelo de dominio que define las credenciales de login.
using EjemploSeguroCapitulo5.Models;

// Importa atributos y clases base para construir controladores API.
using Microsoft.AspNetCore.Mvc;

// Importa tipos y helpers para la creación y validación de tokens JWT.
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

// Importa tipos para definir claims de identidad (Name, Role, etc.).
using System.Security.Claims;

// Proporciona utilidades para trabajar con codificación de texto (clave secreta).
using System.Text;

namespace EjemploSeguroCapitulo5.Controllers
{
    /// <summary>
    /// Controlador responsable de exponer el endpoint de login seguro.
    /// 
    /// Valida credenciales simuladas, genera un JWT con claims
    /// y devuelve el token al cliente para usarlo en peticiones futuras.
    /// </summary>
    [ApiController] // Habilita validación de modelo automática y convenciones REST.
    [Route("[controller]")] // Ruta base: /auth
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Clave secreta utilizada para firmar el JWT.
        /// 
        /// Nota: En entornos reales, esta clave debe almacenarse de forma
        /// segura (variables de entorno, Azure Key Vault, etc.), nunca hardcodeada.
        /// </summary>
        private readonly string _claveSecreta = "SuperClaveUltraMegaHiperSecreta123456789!";

        /// <summary>
        /// Endpoint HTTP POST que recibe credenciales,
        /// valida la autenticidad y emite un JWT firmado.
        /// 
        /// Si las credenciales son inválidas, devuelve HTTP 401.
        /// Si el cuerpo JSON es nulo o incompleto, devuelve HTTP 400.
        /// </summary>
        /// <param name="login">Modelo que contiene nombre de usuario y contraseña.</param>
        /// <returns>
        /// HTTP 200 OK con el token JWT si las credenciales son válidas.
        /// HTTP 401 Unauthorized si son incorrectas.
        /// HTTP 400 Bad Request si el cuerpo es nulo o inválido.
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLogin? login)
        {
            // -------------------------------------------------------
            // VALIDACIÓN ANTI-NULL PARA PREVENIR 500
            // -------------------------------------------------------
            if (login == null)
                return BadRequest("El cuerpo JSON es obligatorio.");

            // -------------------------------------------------------
            // VALIDACIÓN DE CREDENCIALES SIMULADAS
            // -------------------------------------------------------
            // Aquí se compara contra valores fijos para la demo.
            // En producción, se validaría contra una base de datos
            // y se aplicaría hashing seguro a la contraseña.
            if (login.Nombre != "admin" || login.Contrasena != "password123")
                return Unauthorized("Credenciales inválidas.");

            // -------------------------------------------------------
            // CREACIÓN DE CLAIMS
            // -------------------------------------------------------
            // Define la información del usuario que se incluirá en el token.
            var claims = new[]
            {
                // Nombre del usuario
                new Claim(ClaimTypes.Name, login.Nombre),

                // Rol asignado, que luego se usará en [Authorize(Roles = "...")]
                new Claim(ClaimTypes.Role, "Admin")
            };

            // -------------------------------------------------------
            // CONFIGURACIÓN DE LA FIRMA DEL TOKEN
            // -------------------------------------------------------
            // Se genera una clave simétrica basada en la clave secreta.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_claveSecreta));

            // Se crea la firma usando HMAC-SHA256.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // -------------------------------------------------------
            // CREACIÓN DEL JWT
            // -------------------------------------------------------
            // Se construye el token JWT con claims, fecha de expiración y firma.
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            // -------------------------------------------------------
            // DEVOLUCIÓN DEL TOKEN AL CLIENTE
            // -------------------------------------------------------
            // El cliente debe usar este token en el header Authorization: Bearer <token>
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}