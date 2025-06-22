// ================================================================
//   CryptoService.cs — Servicio de Hashing Seguro
// ================================================================
// Ofrece métodos para:
// - Generar hash de contraseñas con salt aleatorio.
// - Verificar contraseñas usando PBKDF2 con SHA256.
// Usado tanto por AccountApiController como por cualquier otra capa.
// ================================================================

// ---------------------------------------------------------------
// Importaciones necesarias:
// ---------------------------------------------------------------
using System;                      // Tipos básicos y Convert.
using System.Security.Cryptography; // PBKDF2, RandomNumberGenerator.
using System.Text;                 // Para codificación si fuera necesario (no usado aquí).

// ---------------------------------------------------------------
// Namespace del proyecto:
// ---------------------------------------------------------------
namespace EjemploSeguroCapitulo9.Services
{
    /// <summary>
    /// Servicio para gestionar hashing y verificación de contraseñas.
    /// Emplea PBKDF2 + SHA256 con 100.000 iteraciones.
    /// </summary>
    public class CryptoService
    {
        /// <summary>
        /// Genera hash + salt para una contraseña dada.
        /// </summary>
        /// <param name="password">La contraseña en texto plano.</param>
        /// <returns>Tupla (hash, salt) codificados en Base64.</returns>
        public (string hash, string salt) HashPassword(string password)
        {
            // Crea salt aleatorio de 16 bytes.
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);

            // Usa PBKDF2 con SHA256 y 100.000 iteraciones.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,               // Contraseña original.
                saltBytes,              // Salt generado.
                100_000,                // Iteraciones.
                HashAlgorithmName.SHA256 // Algoritmo.
            );

            // Genera hash de 32 bytes y codifica en Base64.
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            // Retorna hash y salt en texto Base64.
            return (hash, Convert.ToBase64String(saltBytes));
        }

        /// <summary>
        /// Verifica que una contraseña coincida con un hash y salt dados.
        /// </summary>
        /// <param name="password">Contraseña ingresada.</param>
        /// <param name="salt">Salt en Base64 almacenado.</param>
        /// <param name="hashToCompare">Hash original en Base64 almacenado.</param>
        /// <returns>True si coincide, False si no.</returns>
        public bool VerifyPassword(string password, string salt, string hashToCompare)
        {
            // Decodifica salt desde Base64.
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Recalcula PBKDF2 con los mismos parámetros.
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                100_000,
                HashAlgorithmName.SHA256
            );

            // Genera nuevo hash.
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            // Compara con el hash original.
            return hash == hashToCompare;
        }
    }
}