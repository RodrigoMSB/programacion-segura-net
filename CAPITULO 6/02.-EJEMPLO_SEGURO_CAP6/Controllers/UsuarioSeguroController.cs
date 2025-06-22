// ================================================================
//   UsuarioSeguroController.cs — Controlador (Versión SEGURA)
// ================================================================
// Controlador principal del Ejemplo SEGURO — Capítulo 6.
// Demuestra prácticas seguras de:
//   - Hash de contraseñas con PBKDF2 y salt único
//   - Verificación de hash
//   - Cifrado AES con clave gestionada de forma simulada
//   - Descifrado usando un DTO JSON bien estructurado
//
// Totalmente alineado con las buenas prácticas .NET 8.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres necesarios
// ---------------------------------------------------------------

// Importa los modelos de dominio: UsuarioSeguro y DatoCifradoDTO.
using EjemploSeguroCapitulo6.Models;

// Importa el servicio criptográfico centralizado.
using EjemploSeguroCapitulo6.Services;

// Importa utilidades para definir rutas y respuestas REST.
using Microsoft.AspNetCore.Mvc;

// Proporciona codificación de texto para gestionar claves.
using System.Text;

namespace EjemploSeguroCapitulo6.Controllers
{
    /// <summary>
    /// Controlador seguro que expone endpoints de registro de usuarios,
    /// verificación de contraseñas, cifrado y descifrado de datos sensibles.
    /// Ruta base: /usuario-seguro
    /// </summary>
    [ApiController]
    [Route("usuario-seguro")]
    public class UsuarioSeguroController : ControllerBase
    {
        /// <summary>
        /// Almacena usuarios en memoria de forma simulada para la demo.
        /// Cada usuario tiene su hash y salt propio.
        /// </summary>
        private static List<UsuarioSeguro> _usuarios = new();

        /// <summary>
        /// Servicio criptográfico responsable de hash, verificación,
        /// cifrado y descifrado usando PBKDF2 y AES.
        /// </summary>
        private readonly CryptoService _crypto;

        /// <summary>
        /// Clave simétrica para AES, simulada para la práctica.
        /// En entornos reales, debe gestionarse en un Key Vault.
        /// </summary>
        private readonly byte[] _key;

        /// <summary>
        /// Constructor del controlador.
        /// Recibe el servicio de criptografía por inyección de dependencias.
        /// Inicializa la clave AES simulada.
        /// </summary>
        /// <param name="crypto">Instancia del CryptoService inyectado.</param>
        public UsuarioSeguroController(CryptoService crypto)
        {
            _crypto = crypto;
            // Clave AES de 32 bytes rellenada para cumplir AES-256.
            _key = Encoding.UTF8.GetBytes("ClaveDemoParaAES256Demo!".PadRight(32, '0'));
        }

        /// <summary>
        /// Endpoint POST que registra un usuario de forma segura.
        /// Genera un hash usando PBKDF2 con salt único por usuario.
        /// </summary>
        /// <param name="userInput">Usuario con nombre y password en texto plano.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] UsuarioSeguro userInput)
        {
            // Genera hash + salt con PBKDF2 (100.000 iteraciones)
            var (hash, salt) = _crypto.HashPassword(userInput.HashPassword);

            // Guarda usuario con hash y salt, no guarda password original.
            _usuarios.Add(new UsuarioSeguro
            {
                Nombre = userInput.Nombre,
                HashPassword = hash,
                Salt = salt
            });

            return Ok("Usuario registrado de forma segura con PBKDF2 y salt.");
        }

        /// <summary>
        /// Endpoint POST que verifica la validez de una contraseña.
        /// Compara el hash calculado usando el salt con el hash almacenado.
        /// </summary>
        /// <param name="userInput">Usuario con nombre y password a verificar.</param>
        /// <returns>Mensaje indicando si la password es correcta o no.</returns>
        [HttpPost("verificar")]
        public IActionResult Verificar([FromBody] UsuarioSeguro userInput)
        {
            // Busca usuario en la lista simulada.
            var user = _usuarios.FirstOrDefault(u => u.Nombre == userInput.Nombre);
            if (user == null) return NotFound("Usuario no encontrado.");

            // Verifica password usando el salt almacenado.
            bool ok = _crypto.VerifyPassword(
                userInput.HashPassword,
                user.Salt,
                user.HashPassword
            );

            return Ok(ok ? "Password correcto." : "Password incorrecto.");
        }

        /// <summary>
        /// Endpoint GET que devuelve un dato sensible cifrado con AES.
        /// Usa IV dinámico embebido junto al cipher (buen práctica).
        /// </summary>
        /// <returns>Texto cifrado en Base64.</returns>
        [HttpGet("dato-cifrado")]
        public IActionResult ObtenerDatoCifrado()
        {
            string dato = "Información Muy Sensible (Seguro)";
            string cipher = _crypto.Encrypt(dato, _key);
            return Ok(cipher);
        }

        /// <summary>
        /// Endpoint POST que descifra un dato cifrado enviado como JSON.
        /// Usa un DTO bien tipado para estructurar el payload.
        /// </summary>
        /// <param name="dto">DTO que contiene el texto cifrado en Base64.</param>
        /// <returns>Dato descifrado en texto claro.</returns>
        [HttpPost("dato-descifrado")]
        public IActionResult ObtenerDatoDescifrado([FromBody] DatoCifradoDTO dto)
        {
            string plain = _crypto.Decrypt(dto.Base64, _key);
            return Ok(plain);
        }
    }
}