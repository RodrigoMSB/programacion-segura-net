// =====================================================================================
// CryptoService.cs — Servicio de Criptografía
// =====================================================================================
// Este servicio centraliza las operaciones criptográficas necesarias para el sistema,
// incluyendo el almacenamiento seguro de contraseñas (PBKDF2) y el cifrado simétrico
// (AES-256). Aplica buenas prácticas recomendadas por OWASP.
//
// -------------------------------------------------------------------------------------
// FUNCIONES:
// - HashPassword: Hashea una contraseña con salt y PBKDF2.
// - VerifyPassword: Verifica si una contraseña coincide con su hash almacenado.
// - Encrypt: Cifra información sensible usando AES-256.
// - Decrypt: Descifra información cifrada con AES-256.
//
// -------------------------------------------------------------------------------------
// ALGORTIMOS USADOS:
// - PBKDF2 (con SHA-256) para contraseñas.
// - AES-256 con IV aleatorio para cifrado simétrico.
// - Comparación segura usando FixedTimeEquals para evitar ataques de tiempo.
//
// =====================================================================================

using System.Security.Cryptography;
using System.Text;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Provee funciones de hashing y cifrado para proteger información crítica como
    /// contraseñas e información confidencial. 
    /// </summary>
    public class CryptoService
    {
        // ================================================
        // Parámetros de configuración de PBKDF2
        // ================================================

        private const int SaltSize = 16;         // Tamaño del salt: 128 bits
        private const int KeySize = 32;          // Tamaño del hash resultante: 256 bits
        private const int Iterations = 100_000;  // Iteraciones recomendadas por OWASP 2024

        // ============================================================
        // MÉTODO: HashPassword
        // ============================================================

        /// <summary>
        /// Genera un hash seguro para una contraseña usando PBKDF2 con salt aleatorio.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Tupla con el hash y el salt en Base64.</returns>
        public (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize); // Salt aleatorio
            string salt = Convert.ToBase64String(saltBytes);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256
            );

            string hash = Convert.ToBase64String(pbkdf2.GetBytes(KeySize));
            return (hash, salt);
        }

        // ============================================================
        // MÉTODO: VerifyPassword
        // ============================================================

        /// <summary>
        /// Verifica que una contraseña coincida con su hash almacenado usando PBKDF2.
        /// </summary>
        /// <param name="password">Contraseña ingresada por el usuario.</param>
        /// <param name="salt">Salt almacenado asociado al hash.</param>
        /// <param name="hashToCompare">Hash original a verificar.</param>
        /// <returns>true si la contraseña es válida, false en caso contrario.</returns>
        public bool VerifyPassword(string password, string salt, string hashToCompare)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256
            );

            string hash = Convert.ToBase64String(pbkdf2.GetBytes(KeySize));

            // Comparación constante en tiempo para evitar ataques de temporización
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(hash),
                Encoding.UTF8.GetBytes(hashToCompare)
            );
        }

        // ============================================================
        // MÉTODO: Encrypt (AES-256)
        // ============================================================

        /// <summary>
        /// Cifra un texto plano con AES-256. Genera un IV aleatorio para cada ejecución.
        /// </summary>
        /// <param name="plainText">Texto a cifrar.</param>
        /// <param name="key">Clave simétrica de 256 bits (32 bytes).</param>
        /// <returns>Texto cifrado en Base64 (IV + ciphertext).</returns>
        public string Encrypt(string plainText, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV(); // IV aleatorio

            using var encryptor = aes.CreateEncryptor();

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Combinar IV + Ciphertext
            byte[] combined = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, combined, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, combined, aes.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(combined);
        }

        // ============================================================
        // MÉTODO: Decrypt (AES-256)
        // ============================================================

        /// <summary>
        /// Descifra un texto cifrado con AES-256 previamente generado por este servicio.
        /// </summary>
        /// <param name="cipherTextBase64">Texto cifrado en Base64 (IV + ciphertext).</param>
        /// <param name="key">Clave simétrica de 256 bits usada para cifrar.</param>
        /// <returns>Texto plano original.</returns>
        public string Decrypt(string cipherTextBase64, byte[] key)
        {
            byte[] combined = Convert.FromBase64String(cipherTextBase64);

            using var aes = Aes.Create();
            aes.Key = key;

            // Extraer IV y ciphertext
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherBytes = new byte[combined.Length - iv.Length];

            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combined, iv.Length, cipherBytes, 0, cipherBytes.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}