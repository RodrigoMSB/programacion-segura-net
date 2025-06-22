// ================================================================
//   Usuario.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Define la estructura de un usuario para ilustrar prácticas
// inseguras de almacenamiento de contraseñas.
//
// Este modelo no aplica técnicas seguras como salting, algoritmos
// de derivación de clave ni hashing robusto. Es intencionalmente
// vulnerable para fines educativos.
// ================================================================

namespace EjemploInseguroCapitulo6.Models
{
    /// <summary>
    /// Representa un usuario del sistema.
    /// En esta versión insegura, el campo Password se guarda
    /// como un hash SHA256 sin salt ni iteraciones.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre o identificador del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// Se almacena como hash plano SHA256, sin salting ni
        /// algoritmos de derivación, lo que la hace vulnerable
        /// a ataques de diccionario y fuerza bruta.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
