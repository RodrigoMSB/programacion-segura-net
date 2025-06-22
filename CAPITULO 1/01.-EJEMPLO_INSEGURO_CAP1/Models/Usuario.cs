// ================================================================
//   Usuario.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Este archivo define la entidad de usuario del sistema, pero de forma
// deliberadamente incorrecta. NO incluye validaciones ni medidas básicas
// de seguridad, para ejemplificar vulnerabilidades comunes.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------

namespace EjemploInseguroCapitulo1.Models
{
    /// <summary>
    /// Entidad Usuario (Versión INSEGURA).
    /// Representa un usuario del sistema sin protección de datos
    /// ni control de integridad.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// Generado automáticamente por EF Core (InMemory).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario.
        /// ⚠️ No tiene validación de formato ni longitud mínima.
        /// </summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// ⚠️ Se guarda en texto plano sin hash ni cifrado.
        /// MALA PRÁCTICA intencional para fines demostrativos.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;

        /// <summary>
        /// Rol asignado al usuario.
        /// ⚠️ Sin control de acceso real, susceptible a manipulación.
        /// </summary>
        public string Rol { get; set; } = "Usuario";
    }
}
