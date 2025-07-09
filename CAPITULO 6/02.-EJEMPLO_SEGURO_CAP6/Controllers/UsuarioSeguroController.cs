// ================================================================
// UsuarioSeguroController.cs — Controlador (Versión SEGURA)
// ================================================================
// Controlador principal del Ejemplo SEGURO — Capítulo 6.
// Demuestra prácticas seguras de:
// - Hash de contraseñas con PBKDF2 y salt único.
// - Verificación de hash.
// - Cifrado AES con clave gestionada de forma simulada.
// - Descifrado usando un DTO JSON bien estructurado.
//
// Toda la lógica está alineada con las buenas prácticas en .NET 8,
// separando claramente las preocupaciones de controladores y servicios.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

using EjemploSeguroCapitulo6.Models;    // Modelos de dominio: UsuarioSeguro y DatoCifradoDTO.
using EjemploSeguroCapitulo6.Services;  // Servicio centralizado de criptografía.
using Microsoft.AspNetCore.Mvc;         // Infraestructura para construir APIs REST.
using System.Text;                      // Para convertir strings a bytes y viceversa.

namespace EjemploSeguroCapitulo6.Controllers
{
    /// <summary>
    /// Controlador seguro que expone endpoints para:
    /// - Registrar usuarios con hashing seguro (PBKDF2 + salt).
    /// - Verificar contraseñas hasheadas.
    /// - Cifrar y descifrar datos sensibles usando AES.
    /// 
    /// Ruta base: /usuario-seguro
    /// </summary>
    [ApiController]
    [Route("usuario-seguro")]
    public class UsuarioSeguroController : ControllerBase
    {
        // -------------------------------------------------------------------------
        // ATRIBUTOS PRIVADOS
        // -------------------------------------------------------------------------

        /// <summary>
        /// Lista simulada en memoria para almacenar usuarios registrados.
        /// Cada usuario guarda su hash y salt único.
        /// En producción esto se reemplaza por una base de datos.
        /// </summary>
        private static List<UsuarioSeguro> _usuarios = new();

        /// <summary>
        /// Servicio criptográfico centralizado.
        /// Encapsula lógica de hash PBKDF2 y cifrado/descifrado AES.
        /// Inyectado por dependencias para favorecer la prueba y la modularidad.
        /// </summary>
        private readonly CryptoService _crypto;

        /// <summary>
        /// Clave simétrica de 32 bytes para AES-256.
        /// Simulada para la práctica: en producción se gestiona en un Key Vault.
        /// 
        /// AES-256 requiere una clave de exactamente 32 bytes (256 bits).
        /// Aquí la generamos de forma fija y predecible solo para el ejemplo.
        /// </summary>
        private readonly byte[] _key;

        // -------------------------------------------------------------------------
        // CONSTRUCTOR
        // -------------------------------------------------------------------------

        /// <summary>
        /// Constructor del controlador.
        /// - Recibe el servicio de criptografía mediante inyección de dependencias.
        /// - Inicializa la clave AES simulada para las operaciones de cifrado.
        /// </summary>
        /// <param name="crypto">Instancia de CryptoService inyectada por el framework.</param>
        public UsuarioSeguroController(CryptoService crypto)
        {
            _crypto = crypto;

            // Crear clave AES de 32 bytes (rellenada con ceros para el demo).
            // En escenarios reales debe ser aleatoria, segura y almacenada en un Key Vault.
            _key = Encoding.UTF8.GetBytes("ClaveDemoParaAES256Demo!".PadRight(32, '0'));
        }

        // -------------------------------------------------------------------------
        // ENDPOINT: Registrar
        // -------------------------------------------------------------------------

        /// <summary>
        /// Endpoint POST que registra un usuario de forma segura.
        /// Proceso:
        /// - Genera un hash de la contraseña usando PBKDF2 con salt único.
        /// - Almacena únicamente el hash y el salt, jamás la contraseña en texto plano.
        /// 
        /// Buenas prácticas aplicadas:
        /// - Hashing robusto con 100,000 iteraciones.
        /// - Salt único por usuario para evitar ataques de rainbow tables.
        /// </summary>
        /// <param name="userInput">Objeto JSON con nombre y contraseña en texto claro.</param>
        /// <returns>Mensaje de confirmación de registro exitoso.</returns>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] UsuarioSeguro userInput)
        {
            // Generar hash y salt seguros usando PBKDF2.
            var (hash, salt) = _crypto.HashPassword(userInput.HashPassword);

            // Almacenar el usuario en la lista simulada, guardando solo hash y salt.
            _usuarios.Add(new UsuarioSeguro
            {
                Nombre = userInput.Nombre,
                HashPassword = hash,
                Salt = salt
            });

            return Ok("Usuario registrado de forma segura con PBKDF2 y salt.");
        }

        // -------------------------------------------------------------------------
        // ENDPOINT: Verificar
        // -------------------------------------------------------------------------

        /// <summary>
        /// Endpoint POST que verifica la validez de una contraseña ingresada.
        /// Proceso:
        /// - Busca el usuario registrado por nombre.
        /// - Usa el salt original para recalcular el hash con la contraseña ingresada.
        /// - Compara el hash recalculado con el almacenado.
        /// 
        /// Seguridad:
        /// - Sin el salt correcto, la verificación fallará incluso con la misma contraseña.
        /// - Evita almacenamiento o comparación de contraseñas en texto plano.
        /// </summary>
        /// <param name="userInput">JSON con nombre de usuario y contraseña a verificar.</param>
        /// <returns>200 OK con mensaje indicando si la contraseña es correcta o no.</returns>
        [HttpPost("verificar")]
        public IActionResult Verificar([FromBody] UsuarioSeguro userInput)
        {
            // Buscar el usuario registrado en la lista simulada.
            var user = _usuarios.FirstOrDefault(u => u.Nombre == userInput.Nombre);
            if (user == null) return NotFound("Usuario no encontrado.");

            // Verificar la contraseña ingresada usando el salt original y el hash almacenado.
            bool ok = _crypto.VerifyPassword(
                userInput.HashPassword,
                user.Salt,
                user.HashPassword
            );

            return Ok(ok ? "Password correcto." : "Password incorrecto.");
        }

        // -------------------------------------------------------------------------
        // ENDPOINT: ObtenerDatoCifrado
        // -------------------------------------------------------------------------

        /// <summary>
        /// Endpoint GET que devuelve un dato sensible cifrado con AES.
        /// Buenas prácticas aplicadas:
        /// - IV generado aleatoriamente en cada cifrado.
        /// - IV embebido junto con el ciphertext en el resultado.
        /// 
        /// Resultado:
        /// - Base64 que combina IV + ciphertext.
        /// - Puede almacenarse o transmitirse de forma segura.
        /// </summary>
        /// <returns>Cadena Base64 con IV + ciphertext.</returns>
        [HttpGet("dato-cifrado")]
        public IActionResult ObtenerDatoCifrado()
        {
            string dato = "Información Muy Sensible (Seguro)";

            // Cifrar el dato usando AES con IV dinámico.
            string cipher = _crypto.Encrypt(dato, _key);

            return Ok(cipher);
        }

        // -------------------------------------------------------------------------
        // ENDPOINT: ObtenerDatoDescifrado
        // -------------------------------------------------------------------------

        /// <summary>
        /// Endpoint POST que descifra un dato cifrado enviado como JSON.
        /// Buenas prácticas aplicadas:
        /// - Usa un DTO bien definido para el payload, evitando inputs ambiguos.
        /// - Extrae IV del mensaje cifrado (embebido en el Base64).
        /// - Aplica descifrado AES con clave gestionada.
        /// </summary>
        /// <param name="dto">DTO con propiedad Base64 que contiene IV + ciphertext.</param>
        /// <returns>Texto claro descifrado.</returns>
        [HttpPost("dato-descifrado")]
        public IActionResult ObtenerDatoDescifrado([FromBody] DatoCifradoDTO dto)
        {
            // Descifrar el texto cifrado usando AES.
            string plain = _crypto.Decrypt(dto.Base64, _key);

            return Ok(plain);
        }
    }
}