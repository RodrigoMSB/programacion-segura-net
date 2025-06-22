// ================================================================
//   UsuarioController.cs — Controlador (Versión INSEGURA, REPARADO)
// ================================================================
// Demuestra prácticas criptográficas incorrectas y vulnerables:
// - Hash SHA256 sin salt ni KDF
// - Cifrado con clave y IV fijos embebidos
// - Sin gestión segura de claves
//
// Estructura reparada: evita errores de tipo usando un método privado.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Dependencias del controlador
// ---------------------------------------------------------------

// Importa el modelo Usuario para manipular nombre y contraseña.
using EjemploInseguroCapitulo6.Models;

// Importa clases de ASP.NET Core MVC para definir rutas y respuestas HTTP.
using Microsoft.AspNetCore.Mvc;

// Importa clases de .NET para operaciones criptográficas: SHA256 y AES.
using System.Security.Cryptography;

// Importa utilidades para trabajar con codificación de texto.
using System.Text;

namespace EjemploInseguroCapitulo6.Controllers
{
    /// <summary>
    /// Controlador que gestiona:
    /// - Registro de usuarios con hash inseguro.
    /// - Cifrado de un dato fijo.
    /// - Descifrado del dato usando clave/IV fijos.
    /// 
    /// La ruta base está definida en minúsculas para evitar errores de coincidencia.
    /// </summary>
    [ApiController]
    [Route("usuario")] // Ruta explícita: siempre se accede como /usuario
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Lista estática en memoria para almacenar usuarios.
        /// Simula una "base de datos" básica para demostración.
        /// </summary>
        private static List<Usuario> _usuarios = new();

        /// <summary>
        /// Clave simétrica fija, embebida directamente en el código.
        /// Vulnerabilidad: cualquier persona con acceso al código puede leerla.
        /// </summary>
        private readonly string _claveFija = "ClaveUltraInsegura123!";

        /// <summary>
        /// Vector de Inicialización (IV) fijo y embebido.
        /// Vulnerabilidad: reutilizar el mismo IV anula la seguridad de AES.
        /// </summary>
        private readonly byte[] _ivFijo = Encoding.UTF8.GetBytes("1234567890123456");

        /// <summary>
        /// Endpoint para registrar un usuario.
        /// Aplica un hash SHA256 a la contraseña SIN salt ni KDF, práctica insegura.
        /// </summary>
        /// <param name="usuario">Objeto JSON con nombre y contraseña.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            // ---------------------------------------------------------------
            // Crear instancia de SHA256 para procesar la contraseña.
            // ---------------------------------------------------------------
            using var sha = SHA256.Create();

            // Convertir la contraseña de texto plano a bytes.
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(usuario.Password));

            // Convertir el hash a Base64 para almacenarlo de forma legible.
            usuario.Password = Convert.ToBase64String(hash);

            // Guardar el usuario en la lista.
            _usuarios.Add(usuario);

            // Responder al cliente con mensaje explícito.
            return Ok("Usuario registrado de forma insegura (hash SHA256 sin salt).");
        }

        /// <summary>
        /// Endpoint para cifrar un dato fijo.
        /// Utiliza clave e IV fijos, demostrando mala práctica.
        /// </summary>
        /// <returns>Dato cifrado codificado en Base64.</returns>
        [HttpGet("dato-cifrado")]
        public IActionResult ObtenerDatoCifrado()
        {
            // Llama a método privado que encapsula la lógica de cifrado.
            string cifradoBase64 = CifrarDatoFijo();

            // Responde con el dato cifrado.
            return Ok(cifradoBase64);
        }

        /// <summary>
        /// Endpoint para descifrar el dato previamente cifrado.
        /// Reutiliza la misma clave e IV fijos.
        /// </summary>
        /// <returns>Texto original descifrado.</returns>
        [HttpGet("dato-descifrado")]
        public IActionResult ObtenerDatoDescifrado()
        {
            // Generar el texto cifrado usando el método privado.
            string cifradoBase64 = CifrarDatoFijo();

            // Convertir el Base64 de vuelta a bytes.
            byte[] cipherBytes = Convert.FromBase64String(cifradoBase64);

            // Crear instancia de AES para descifrado.
            using var aes = Aes.Create();

            // Configurar clave y IV igual que en el cifrado.
            aes.Key = Encoding.UTF8.GetBytes(_claveFija.PadRight(32, '0'));
            aes.IV = _ivFijo;

            // Crear descifrador simétrico.
            using var decryptor = aes.CreateDecryptor();

            // Aplicar descifrado y recuperar bytes originales.
            byte[] plaintext = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            // Convertir bytes a texto legible y devolverlo.
            return Ok(Encoding.UTF8.GetString(plaintext));
        }

        /// <summary>
        /// Método privado que encapsula la lógica de cifrado.
        /// Reutilizado para evitar duplicar la misma operación.
        /// </summary>
        /// <returns>Texto cifrado en Base64.</returns>
        private string CifrarDatoFijo()
        {
            // Texto que se va a cifrar.
            string dato = "Información Muy Sensible";

            // Crear instancia de AES.
            using var aes = Aes.Create();

            // Configurar clave de 32 bytes para AES-256.
            aes.Key = Encoding.UTF8.GetBytes(_claveFija.PadRight(32, '0'));

            // Configurar IV fijo.
            aes.IV = _ivFijo;

            // Crear cifrador.
            using var encryptor = aes.CreateEncryptor();

            // Convertir texto a bytes.
            byte[] plaintext = Encoding.UTF8.GetBytes(dato);

            // Aplicar cifrado.
            byte[] cipherText = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);

            // Devolver resultado como Base64.
            return Convert.ToBase64String(cipherText);
        }
    }
}