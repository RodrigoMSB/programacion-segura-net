using System.Security.Cryptography;
using System.Text;

namespace SeguridadBancoFinal.Services
{
    public class CryptoService
    {
        // ================================
        // Configuración de PBKDF2
        // ================================
        private const int SaltSize = 16;         // 128 bits
        private const int KeySize = 32;          // 256 bits
        private const int Iterations = 100_000;  // Recomendado por OWASP

        // ================================
        // Generar Hash de Contraseña
        // ================================
        public (string hash, string salt) HashPassword(string password)
        {
            // Generar salt aleatorio seguro
            byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
            string salt = Convert.ToBase64String(saltBytes);

            // Aplicar PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256
            );

            string hash = Convert.ToBase64String(pbkdf2.GetBytes(KeySize));

            return (hash, salt);
        }

        // ================================
        // Verificar Contraseña Hasheada
        // ================================
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

            // Comparación segura constante
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(hash),
                Encoding.UTF8.GetBytes(hashToCompare)
            );
        }

        // ================================
        // Cifrado AES-256
        // ================================
        public string Encrypt(string plainText, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Combinar IV + Ciphertext en un solo array para almacenamiento
            byte[] combined = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, combined, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, combined, aes.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(combined);
        }

        // ================================
        // Descifrado AES-256
        // ================================
        public string Decrypt(string cipherTextBase64, byte[] key)
        {
            byte[] combined = Convert.FromBase64String(cipherTextBase64);

            using var aes = Aes.Create();
            aes.Key = key;

            // Extraer IV del bloque combinado
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