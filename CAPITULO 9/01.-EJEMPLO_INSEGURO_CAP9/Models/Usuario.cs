// ================================================================
//   Usuario.cs — Entidad de dominio (Versión INSEGURA)
// ================================================================
// Define la estructura básica de un usuario para este micro front-end.
// 
// ⚠️ Nota: Esta implementación es deliberadamente insegura:
// - No valida los campos.
// - La contraseña se maneja en texto plano.
// - No hay hash ni cifrado.
//
// Se incluye para mantener la coherencia arquitectónica del proyecto.
// ================================================================

namespace EjemploInseguroCapitulo9.Models
{
    /// <summary>
    /// Representa un usuario del sistema.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre de usuario o alias utilizado para autenticación.
        /// Sin restricciones ni validación de longitud.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// Se almacena y procesa en texto claro (mala práctica intencional).
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
