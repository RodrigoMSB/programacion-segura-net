// ================================================================
// CryptoService.cs — Servicio de Criptografía (Versión SEGURA)
// ================================================================
// Este archivo define un servicio reutilizable que implementa:
// - Hash de contraseñas con PBKDF2 + salt aleatorio.
// - Verificación de hash.
// - Cifrado AES con clave simétrica y IV dinámico.
// - Descifrado AES utilizando el IV embebido en el resultado.
//
// Toda la lógica criptográfica está centralizada y separada de los
// controladores o la UI, siguiendo el principio de responsabilidad única. 
//
// ---------------------------------------------------------------
// GLOSARIO DE CONCEPTOS CLAVE
// ---------------------------------------------------------------
// - Hash: Transformación unidireccional de datos que produce un valor fijo.
//   No reversible. Usado para verificar contraseñas sin almacenarlas en claro.
//
// - Salt: Valor aleatorio único añadido antes de hashear.
//   Evita ataques de rainbow tables. Hace que dos contraseñas iguales
//   produzcan hashes distintos.
//
// - PBKDF2: Algoritmo de derivación de claves con muchas iteraciones.
//   Aumenta la resistencia a ataques de fuerza bruta.
//
// - IV (Vector de Inicialización): Valor aleatorio para cifrado simétrico
//   que garantiza que textos idénticos generen resultados cifrados distintos.
//
// - AES: Estándar de cifrado simétrico (Advanced Encryption Standard),
//   seguro y ampliamente utilizado.
//
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Librerías de .NET para criptografía y codificación.
// ---------------------------------------------------------------

using System.Security.Cryptography; // Provee PBKDF2, AES, RandomNumberGenerator.
using System.Text; // Permite codificar strings a bytes y viceversa.

namespace EjemploSeguroCapitulo6.Services
{
    /// <summary>
    /// Servicio dedicado a operaciones criptográficas críticas.
    /// Encapsula de forma limpia el hashing de contraseñas y
    /// el cifrado/descifrado de datos sensibles.
    /// Facilita la reutilización y evita errores al duplicar lógica criptográfica.
    /// </summary>
    public class CryptoService
    {
        // -------------------------------------------------------------------------
        // MÉTODO: HashPassword
        // -------------------------------------------------------------------------

        /// <summary>
        /// Genera un hash seguro para una contraseña utilizando PBKDF2 con SHA256
        /// y un salt aleatorio único por usuario.
        /// 
        /// Conceptos usados:
        /// - Salt: Asegura que contraseñas iguales generen hashes distintos.
        /// - PBKDF2: Aplica hashing iterativo (100,000 veces) para ralentizar
        ///   ataques de fuerza bruta.
        /// - SHA256: Algoritmo de hash seguro usado internamente.
        /// 
        /// Devuelve tanto el hash como el salt en Base64 para almacenamiento.
        /// </summary>
        /// <param name="password">Contraseña original en texto claro.</param>
        /// <returns>Tupla (hash, salt) en Base64.</returns>
        public (string hash, string salt) HashPassword(string password)
        {
            // Generar un salt aleatorio de 16 bytes.
            // Salt evita ataques de rainbow tables y fuerza hashes distintos.
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);

            // Convertir el salt a Base64 para almacenarlo junto al usuario.
            string salt = Convert.ToBase64String(saltBytes);

            // Crear PBKDF2: combina contraseña + salt + iteraciones.
            // 100,000 iteraciones incrementan el costo de ataques.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                100_000,
                HashAlgorithmName.SHA256
            );

            // Generar hash de 32 bytes (256 bits) y codificar en Base64.
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            return (hash, salt);
        }

        // -------------------------------------------------------------------------
        // MÉTODO: VerifyPassword
        // -------------------------------------------------------------------------

        /// <summary>
        /// Verifica si una contraseña ingresada coincide con el hash almacenado,
        /// usando el salt original.
        /// 
        /// Proceso:
        /// - Decodifica el salt.
        /// - Recalcula el hash con la contraseña ingresada + el salt original.
        /// - Compara el resultado con el hash almacenado.
        /// </summary>
        /// <param name="password">Contraseña ingresada en texto claro.</param>
        /// <param name="salt">Salt original en Base64.</param>
        /// <param name="hashToCompare">Hash almacenado en Base64.</param>
        /// <returns>True si la contraseña es válida; false si no.</returns>
        public bool VerifyPassword(string password, string salt, string hashToCompare)
        {
            // Decodificar el salt desde Base64.
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Re-derivar el hash con la misma configuración.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                100_000,
                HashAlgorithmName.SHA256
            );

            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            // Comparar el hash calculado con el almacenado.
            return hash == hashToCompare;
        }

        // -------------------------------------------------------------------------
        // MÉTODO: Encrypt
        // -------------------------------------------------------------------------

        /// <summary>
        /// Cifra un texto claro utilizando AES en modo seguro.
        /// 
        /// Conceptos usados:
        /// - AES: Cifrado simétrico ampliamente adoptado.
        /// - Clave simétrica: Misma clave para cifrar y descifrar.
        /// - IV (Vector de Inicialización): Aleatorio por mensaje para garantizar
        ///   unicidad y evitar patrones repetidos en salidas cifradas.
        /// 
        /// Estrategia:
        /// - Genera un IV único por cifrado.
        /// - Cifra el texto.
        /// - Combina IV + ciphertext para que puedan almacenarse o transmitirse juntos.
        /// - Devuelve resultado en Base64.
        /// </summary>
        /// <param name="plainText">Texto en claro a cifrar.</param>
        /// <param name="key">Clave simétrica de 32 bytes (AES-256).</param>
        /// <returns>Cadena Base64 con IV + ciphertext.</returns>
        public string Encrypt(string plainText, byte[] key)
        {
            using var aes = Aes.Create();

            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // -------------------------------------------------------------------------
            // COMBINAR IV + CIPHERTEXT EN UN SOLO ARRAY
            // -------------------------------------------------------------------------
            //
            // ¿Por qué hacemos esto?
            // AES necesita el mismo IV para descifrar correctamente.
            // El IV no es secreto, pero es único por mensaje y esencial.
            // 
            // Estrategia común:
            // - Concatenar IV + ciphertext en un solo array.
            // - Así, el receptor puede extraer el IV del inicio y usarlo
            //   para descifrar sin requerir almacenamiento separado.
            //
            // Estructura resultante:
            // [ IV (16 bytes) | Ciphertext (... bytes) ]
            // -------------------------------------------------------------------------

            byte[] combined = new byte[aes.IV.Length + cipherBytes.Length];

            // Copiar IV al inicio.
            Buffer.BlockCopy(aes.IV, 0, combined, 0, aes.IV.Length);

            // Copiar ciphertext inmediatamente después del IV.
            Buffer.BlockCopy(cipherBytes, 0, combined, aes.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(combined);
        }

        // -------------------------------------------------------------------------
        // MÉTODO: Decrypt
        // -------------------------------------------------------------------------

        /// <summary>
        /// Descifra un texto previamente cifrado con el método Encrypt.
        /// 
        /// Conceptos clave:
        /// - AES: Algoritmo de cifrado simétrico que usa la misma clave para cifrar y descifrar.
        /// - IV (Vector de Inicialización): Valor único y aleatorio generado durante el cifrado.
        ///   Es esencial para la operación segura del descifrado, pero no es secreto.
        /// 
        /// Flujo detallado del proceso:
        /// 1️⃣ El método recibe una cadena en Base64 que incluye IV + ciphertext.
        /// 2️⃣ Decodifica el Base64 para recuperar los bytes reales.
        /// 3️⃣ Extrae el IV de los primeros bytes del array combinado.
        /// 4️⃣ Usa la clave proporcionada y el IV para inicializar el descifrador AES.
        /// 5️⃣ Aplica el descifrado para obtener los bytes originales del texto claro.
        /// 6️⃣ Convierte el resultado a UTF-8 y lo devuelve.
        /// 
        /// Notas de seguridad:
        /// - Sin el IV correcto, el descifrado fallará o generará datos corruptos.
        /// - Incluir el IV en el mensaje cifrado es una práctica estándar y segura,
        ///   porque el IV no es secreto, pero debe ser único por cada operación.
        /// </summary>
        /// <param name="cipherTextBase64">Cadena en Base64 que contiene IV + ciphertext.</param>
        /// <param name="key">Clave simétrica de 32 bytes utilizada para descifrar (AES-256).</param>
        /// <returns>Texto claro descifrado.</returns>
        public string Decrypt(string cipherTextBase64, byte[] key)
        {
            // -------------------------------------------------------
            // 1️⃣ Decodificar la cadena Base64 a un array de bytes.
            //    Recupera el formato binario original (IV + ciphertext).
            // -------------------------------------------------------
            byte[] combined = Convert.FromBase64String(cipherTextBase64);

            using var aes = Aes.Create();
            aes.Key = key;

            // -------------------------------------------------------
            // 2️⃣ Extraer el IV del inicio del array combinado.
            //    El IV fue almacenado en los primeros N bytes durante Encrypt().
            //    AES.BlockSize está en bits, así que se divide por 8 para bytes.
            // -------------------------------------------------------
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherBytes = new byte[combined.Length - iv.Length];

            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combined, iv.Length, cipherBytes, 0, cipherBytes.Length);

            // -------------------------------------------------------
            // 3️⃣ Configurar el IV extraído en la instancia de AES.
            //    Es esencial usar exactamente el mismo IV para descifrar.
            // -------------------------------------------------------
            aes.IV = iv;

            // -------------------------------------------------------
            // 4️⃣ Crear el objeto descifrador con la clave y el IV.
            //    Permite aplicar la operación inversa del cifrado.
            // -------------------------------------------------------
            using var decryptor = aes.CreateDecryptor();

            // -------------------------------------------------------
            // 5️⃣ Aplicar el descifrado al bloque de ciphertext.
            //    Obtiene los bytes originales del texto claro.
            // -------------------------------------------------------
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            // -------------------------------------------------------
            // 6️⃣ Convertir los bytes resultantes en un string UTF-8.
            //    Devuelve el texto claro original al consumidor.
            // -------------------------------------------------------
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}