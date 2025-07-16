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
            // --------------------------------------------
            // Crear una instancia del algoritmo AES.
            // AES.Create() devuelve un objeto configurado con valores seguros por defecto.
            // --------------------------------------------
            using var aes = Aes.Create();

            // --------------------------------------------
            // Asignar la clave de 32 bytes (AES-256).
            // La misma clave deberá usarse para descifrar.
            // --------------------------------------------
            aes.Key = key;

            // --------------------------------------------
            // Generar un IV (Vector de Inicialización) aleatorio.
            // El IV asegura que el mismo texto cifrado dos veces produzca resultados distintos.
            // --------------------------------------------
            aes.GenerateIV();

            // --------------------------------------------
            // Crear el objeto cifrador (encryptor) con la clave y el IV actuales.
            // Este objeto se encargará de aplicar el cifrado sobre los datos.
            // --------------------------------------------
            using var encryptor = aes.CreateEncryptor();

            // --------------------------------------------
            // Convertir el texto claro a un arreglo de bytes.
            // AES opera sobre datos binarios, no sobre strings directamente.
            // --------------------------------------------
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // --------------------------------------------
            // Aplicar el cifrado al bloque de datos.
            // --------------------------------------------
            // AES en .NET usa el modelo de cifrado por bloques. El método
            // TransformFinalBlock() recibe los datos en claro como un arreglo de bytes
            // y devuelve un nuevo arreglo de bytes con el contenido cifrado.
            //
            // Parámetros:
            // - plainBytes: el array de bytes que representa el texto claro a cifrar.
            // - 0: posición inicial en el array (inicio).
            // - plainBytes.Length: número de bytes a procesar (todo el contenido).
            //
            // Características clave:
            // - Procesa TODO el bloque de datos en una sola llamada.
            // - Maneja automáticamente el relleno (padding) necesario para cumplir
            //   con el tamaño de bloque de AES (16 bytes por bloque).
            // - Devuelve el contenido cifrado listo para almacenamiento o transmisión.
            //
            // Ejemplo conceptual:
            // Input  -> "HolaMundo" (bytes)
            // Output -> [cifrado en bytes incomprensibles]
            //
            // Seguridad:
            // - Sin la clave y el IV correctos, el contenido cifrado no puede revertirse
            //   a su forma original.
            //
            // Resultado:
            // - cipherBytes contendrá los datos cifrados, que se combinarán con el IV
            //   para el almacenamiento seguro.
            //
            // Código:
            // --------------------------------------------
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
        /// 1 El método recibe una cadena en Base64 que incluye IV + ciphertext.
        /// 2 Decodifica el Base64 para recuperar los bytes reales.
        /// 3 Extrae el IV de los primeros bytes del array combinado.
        /// 4 Usa la clave proporcionada y el IV para inicializar el descifrador AES.
        /// 5 Aplica el descifrado para obtener los bytes originales del texto claro.
        /// 6 Convierte el resultado a UTF-8 y lo devuelve.
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

            // ---------------------------------------------------------------
            // 2️⃣ Extraer el IV del inicio del array combinado.
            // ---------------------------------------------------------------
            // Cuando ciframos en el método Encrypt, combinamos IV + ciphertext
            // en un solo array. Aquí, para descifrar, debemos separar de nuevo
            // esos componentes.
            //
            // AES.BlockSize indica el tamaño del bloque en BITS (128 bits por defecto),
            // por eso se divide por 8 para obtener bytes (16 bytes para AES).
            //
            // Por ejemplo:
            //   combined = [IV (16 bytes) | Ciphertext (variable)]
            //
            // - IV: Necesario para inicializar correctamente el descifrador AES.
            //   Es único por mensaje pero NO SECRETO. Permite que se pueda descifrar
            //   correctamente incluso si se transmite junto con el ciphertext.
            //
            // - Ciphertext: Los datos cifrados reales que se quieren recuperar.
            //
            // Proceso detallado:
            // ---------------------------------------------------------------
            byte[] iv = new byte[aes.BlockSize / 8];                   // Reservamos espacio para el IV (16 bytes).
            byte[] cipherBytes = new byte[combined.Length - iv.Length]; // Reservamos espacio para el ciphertext restante.

            // Copiar el IV desde el inicio del array combinado.
            //   combined: [IV | Ciphertext]
            //   Resultado: iv = combined[0 .. 15]
            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);

            // Copiar el ciphertext desde la posición siguiente al IV.
            //   combined: [IV | Ciphertext]
            //   Resultado: cipherBytes = combined[16 .. end]
            Buffer.BlockCopy(combined, iv.Length, cipherBytes, 0, cipherBytes.Length);

            // ---------------------------------------------------------------
            // En resumen:
            //   - iv: contiene el Vector de Inicialización usado en el cifrado original.
            //   - cipherBytes: contiene los datos cifrados reales.
            //
            // Al extraer correctamente ambos, podemos inicializar AES
            // con la misma configuración para descifrar con éxito.
            // ---------------------------------------------------------------

            // -------------------------------------------------------
            // Configurar el IV extraído en la instancia de AES.
            // Es esencial usar exactamente el mismo IV para descifrar.
            // -------------------------------------------------------
            aes.IV = iv;

            // -------------------------------------------------------
            // Crear el objeto descifrador con la clave y el IV.
            // Permite aplicar la operación inversa del cifrado.
            // -------------------------------------------------------
            using var decryptor = aes.CreateDecryptor();

            // ---------------------------------------------------------------
            // 5️⃣ Aplicar el descifrado al bloque de ciphertext.
            // ---------------------------------------------------------------
            // Aquí usamos el objeto `decryptor`, configurado previamente con:
            //   - La clave AES (misma usada en el cifrado original)
            //   - El IV extraído del mensaje cifrado
            //
            // AES en .NET expone el método TransformFinalBlock:
            //   - Toma un array de bytes de entrada (cipherBytes)
            //   - Devuelve el array de bytes descifrados (plainBytes)
            //
            // Proceso detallado:
            //   - AES usa la clave y el IV para "desenrollar" el cifrado.
            //   - Aplica la operación inversa del cifrado de bloque.
            //   - Devuelve los datos originales en claro tal cual fueron
            //     antes de ser cifrados.
            //
            // Ejemplo conceptual:
            //   - Entrada: ciphertext en bytes.
            //   - Salida: texto plano en bytes.
            //
            // Seguridad:
            //   - Solo quien posea la clave correcta puede descifrar.
            //   - IV garantiza unicidad, pero la clave asegura confidencialidad.
            //
            // Resultado final:
            //   - plainBytes contiene los datos originales,
            //     listos para ser convertidos de vuelta a string (UTF-8).
            // ---------------------------------------------------------------
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            // -------------------------------------------------------
            // Convertir los bytes resultantes en un string UTF-8.
            // Devuelve el texto claro original al consumidor.
            // -------------------------------------------------------
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}