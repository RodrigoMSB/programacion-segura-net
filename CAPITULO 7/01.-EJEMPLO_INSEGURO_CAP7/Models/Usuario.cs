// ================================================================
//   Usuario.cs — Entidad de Dominio (Versión INSEGURA, Capítulo 7)
// ================================================================
// Este archivo define la entidad Usuario de forma mínima.
// En este ejemplo se usa como placeholder para mantener la
// consistencia de estructura del dominio, aunque la lógica de
// sesiones no utiliza atributos adicionales.
// ================================================================

namespace EjemploInseguroCapitulo7.Models
{
    /// <summary>
    /// Entidad Usuario para representar un usuario dentro del
    /// dominio de la aplicación. 
    /// 
    /// En este capítulo se mantiene simple, demostrando
    /// coherencia con la estructura de Models.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre de usuario. En un sistema real, se
        /// complementaría con ID, email, roles, etc.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}