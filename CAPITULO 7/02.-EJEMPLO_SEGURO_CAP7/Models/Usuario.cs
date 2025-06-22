// ================================================================
//   Usuario.cs — Entidad de Dominio (Versión SEGURA)
// ================================================================
// Define la entidad Usuario para mantener la coherencia estructural
// del dominio de la aplicación, incluso si no se usa directamente
// en la lógica de sesión. Permite mantener una arquitectura limpia
// y extensible a futuro.
// ================================================================

namespace EjemploSeguroCapitulo7.Models
{
    /// <summary>
    /// Representa un Usuario dentro del dominio.
    /// 
    /// Esta clase es intencionalmente simple en este capítulo,
    /// pero sirve como base para ilustrar cómo se organiza
    /// la carpeta Models de forma coherente en toda la solución.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre del usuario. 
        /// En una aplicación real, esta clase incluiría
        /// más propiedades (ID, Email, Roles, Claims, etc.).
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}