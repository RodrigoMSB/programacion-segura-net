// ================================================================
//   UsuarioSeguro.cs — Modelo de dominio (Versión SEGURO)
// ================================================================
// Define la estructura de un usuario registrado de forma segura:
// - Almacena nombre de usuario.
// - Guarda el hash de la contraseña usando PBKDF2.
// - Guarda el salt único generado para cada usuario.
//
// Este modelo refleja prácticas recomendadas de almacenamiento de credenciales.
// ================================================================

namespace EjemploSeguroCapitulo6.Models
{
    /// <summary>
    /// Modelo de dominio para usuarios registrados con prácticas seguras.
    /// Contiene:
    /// - Nombre del usuario.
    /// - Hash de la contraseña derivado mediante PBKDF2.
    /// - Salt aleatorio asociado al hash.
    /// 
    /// No almacena la contraseña en texto plano.
    /// </summary>
    public class UsuarioSeguro
    {
        /// <summary>
        /// Nombre del usuario registrado.
        /// Este campo se usa como identificador lógico.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Hash de la contraseña generado mediante PBKDF2.
        /// Se guarda en formato Base64.
        /// Nunca se almacena la contraseña original.
        /// </summary>
        public string HashPassword { get; set; } = string.Empty;

        /// <summary>
        /// Salt único generado para este usuario.
        /// Se utiliza junto con la contraseña para derivar el hash.
        /// Se guarda en Base64.
        /// </summary>
        public string Salt { get; set; } = string.Empty;
    }
}
