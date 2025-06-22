// ================================================================
//   CryptoService.cs — Servicio de Criptografía (Versión SEGURO)
// ================================================================
// Implementa buenas prácticas para:
// - Hash de contraseñas con PBKDF2 + salt aleatorio.
// - Verificación de hash.
// - Cifrado AES con clave simétrica y IV dinámico.
// - Descifrado AES utilizando el IV embebido.
// 
// Todo gestionado de forma reutilizable para mantener la lógica
// criptográfica separada de los controladores.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Librerías de .NET para criptografía y codificación.
// ---------------------------------------------------------------

using System.Security.Cryptography; // Provee PBKDF2, AES, RandomNumberGenerator.
using System.Text; // Provee codificación de strings a bytes y viceversa.

namespace EjemploSeguroCapitulo6.Services
{
    /// <summary>
    /// Servicio que encapsula toda la lógica criptográfica de la aplicación.
    /// Reutilizable para hash de contraseñas y cifrado de datos sensibles.
    /// </summary>
    public class CryptoService
    {
        /// <summary>
        /// Genera un hash seguro de la contraseña usando PBKDF2 con salt único.
        /// 
        /// - Usa SHA256 como función de hash interna.
        /// - Aplica 100,000 iteraciones para retardar ataques de fuerza bruta.
        /// </summary>
        /// <param name="password">Contraseña original en texto claro.</param>
        /// <returns>Tupla (hash, salt) ambos en Base64.</returns>
        public (string hash, string salt) HashPassword(string password)
        {
            // Generar salt aleatorio de 16 bytes.
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);

            // Convertir salt a Base64 para almacenarlo junto al usuario.
            string salt = Convert.ToBase64String(saltBytes);

            // Crear PBKDF2: combina password + salt + iteraciones.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                100_000, // Número de iteraciones recomendadas.
                HashAlgorithmName.SHA256 // Algoritmo de hash interno.
            );

            // Generar hash de 32 bytes (256 bits) y codificar en Base64.
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            // Devolver hash y salt juntos.
            return (hash, salt);
        }

        /// <summary>
        /// Verifica que una contraseña en texto claro coincide con su hash almacenado.
        /// 
        /// Vuelve a derivar el hash con el salt original y lo compara.
        /// </summary>
        /// <param name="password">Contraseña ingresada por el usuario.</param>
        /// <param name="salt">Salt original en Base64.</param>
        /// <param name="hashToCompare">Hash almacenado en Base64.</param>
        /// <returns>True si la contraseña es correcta, false si no.</returns>
        public bool VerifyPassword(string password, string salt, string hashToCompare)
        {
            // Decodificar salt de Base64 a bytes.
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Crear PBKDF2 usando la misma configuración y salt.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                100_000,
                HashAlgorithmName.SHA256
            );

            // Derivar hash y codificarlo.
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            // Comparar con el hash original.
            return hash == hashToCompare;
        }

        /// <summary>
        /// Cifra un texto claro usando AES con:
        /// - Clave simétrica provista por parámetro.
        /// - IV aleatorio generado para cada cifrado.
        /// 
        /// Devuelve Base64 combinando IV + ciphertext.
        /// </summary>
        /// <param name="plainText">Texto en claro a cifrar.</param>
        /// <param name="key">Clave simétrica de 32 bytes para AES-256.</param>
        /// <returns>Texto cifrado en Base64 (IV + Ciphertext).</returns>
        public string Encrypt(string plainText, byte[] key)
        {
            // Crear instancia AES.
            using var aes = Aes.Create();

            // Configurar clave provista externamente.
            aes.Key = key;

            // Generar IV aleatorio (único por mensaje).
            aes.GenerateIV();

            // Crear cifrador.
            using var encryptor = aes.CreateEncryptor();

            // Convertir texto a bytes.
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Aplicar cifrado.
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Combinar IV + Ciphertext en un solo array.
            byte[] combined = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, combined, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, combined, aes.IV.Length, cipherBytes.Length);

            // Devolver como Base64.
            return Convert.ToBase64String(combined);
        }

        /// <summary>
        /// Descifra un texto previamente cifrado con AES.
        /// Extrae IV embebido y lo usa para descifrar.
        /// </summary>
        /// <param name="cipherTextBase64">Texto cifrado en Base64 (IV + Ciphertext).</param>
        /// <param name="key">Clave simétrica de 32 bytes para AES-256.</param>
        /// <returns>Texto claro descifrado.</returns>
        public string Decrypt(string cipherTextBase64, byte[] key)
        {
            // Decodificar Base64 a bytes.
            byte[] combined = Convert.FromBase64String(cipherTextBase64);

            // Crear instancia AES.
            using var aes = Aes.Create();
            aes.Key = key;

            // Extraer IV del bloque combinado.
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherBytes = new byte[combined.Length - iv.Length];

            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combined, iv.Length, cipherBytes, 0, cipherBytes.Length);

            aes.IV = iv;

            // Crear descifrador.
            using var decryptor = aes.CreateDecryptor();

            // Aplicar descifrado.
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            // Devolver texto claro.
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}