// ===============================================
//   AutenticacionService.cs
// ===============================================
// Este archivo define la lógica de autenticación y validación de contraseñas.
// Forma parte de la capa de servicios de la aplicación.
//
// Contenido pedagógico:
// - Uso de criptografía moderna (PBKDF2)
// - Separación de responsabilidades
// - Validación de entrada y comparación segura
// ===============================================

// -------------------------------------------------------------------------
// IMPORTS
// -------------------------------------------------------------------------

// Librería para operaciones criptográficas robustas (sal, PBKDF2, hash seguro)
using System.Security.Cryptography;

// Librería para manipular y codificar texto (UTF-8, Base64)
using System.Text;

// Importa los modelos de dominio de la aplicación. 
// En este archivo se usa para coherencia con el namespace del proyecto.
using EjemploSeguridadCapitulo1.Models;

namespace EjemploSeguridadCapitulo1.Services
{
    /// <summary>
    /// Servicio responsable de gestionar la autenticación de usuarios.
    /// Incluye lógica para:
    /// - Generar hash de contraseñas con PBKDF2 + sal
    /// - Validar credenciales ingresadas contra el hash almacenado
    /// 
    /// Este servicio NO interactúa directamente con la base de datos.
    /// Está diseñado para ser invocado por controladores o capas de aplicación.
    /// Principio aplicado: responsabilidad única.
    /// </summary>
    public class AutenticacionService
    {
        // -------------------------------------------------------------------------
        // CONSTANTES DE CONFIGURACIÓN
        // -------------------------------------------------------------------------

        /// <summary>
        /// Tamaño de la sal (salt) en bytes. 
        /// 16 bytes = 128 bits, recomendado para PBKDF2.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Tamaño de la clave derivada (hash) en bytes.
        /// 32 bytes = 256 bits, adecuado para seguridad robusta.
        /// </summary>
        private const int KeySize = 32;

        /// <summary>
        /// Número de iteraciones para PBKDF2.
        /// Recomendación de OWASP: mínimo 100,000 para evitar ataques de fuerza bruta.
        /// </summary>
        private const int Iterations = 100_000;

        // -------------------------------------------------------------------------
        // MÉTODOS PÚBLICOS
        // -------------------------------------------------------------------------

        /// <summary>
        /// Genera un hash seguro para una contraseña de entrada.
        /// Algoritmo usado: PBKDF2 con SHA-256 y sal aleatoria.
        /// 
        /// Formato de retorno:
        /// {sal_base64}.{hash_base64}
        /// </summary>
        /// <param name="password">Contraseña original en texto plano.</param>
        /// <returns>String codificado con sal y hash.</returns>
        public string HashearPassword(string password)
        {
            // Validación defensiva de entrada
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            // Generar sal aleatoria usando RNG seguro
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Derivar clave usando PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            // Combinar sal y hash codificados en Base64
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifica si una contraseña ingresada coincide con el hash almacenado.
        /// Usa PBKDF2 + comparación de tiempo constante para evitar ataques de timing.
        /// 
        /// Formato esperado del hash almacenado: {sal_base64}.{hash_base64}
        /// </summary>
        /// <param name="password">Contraseña ingresada por el usuario.</param>
        /// <param name="hashAlmacenado">Hash almacenado con sal.</param>
        /// <returns>True si coincide; False si no coincide o hay error de formato.</returns>
        public bool VerificarPassword(string password, string hashAlmacenado)
        {
            // Validación de entrada
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashAlmacenado))
                return false;

            try
            {
                // Separar sal y hash por el delimitador '.'
                var partes = hashAlmacenado.Split('.');
                if (partes.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(partes[0]);
                byte[] hashOriginal = Convert.FromBase64String(partes[1]);

                // Derivar hash con la misma sal
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                byte[] hashIntento = pbkdf2.GetBytes(KeySize);

                // Comparar en tiempo constante para mitigar ataques de timing
                return CryptographicOperations.FixedTimeEquals(hashIntento, hashOriginal);
            }
            catch
            {
                // Si falla por formato inválido o error de decodificación, retornar false
                return false;
            }
        }
    }
}