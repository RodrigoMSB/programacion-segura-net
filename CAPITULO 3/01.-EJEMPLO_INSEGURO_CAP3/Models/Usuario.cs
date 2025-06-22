// ================================================================
//   Usuario.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Este archivo define la clase Usuario para el ejemplo inseguro del
// Capítulo 3. Muestra malas prácticas comunes:
// - No aplica validación de campos.
// - Contiene una clave por defecto escrita en texto plano.
// - Asigna rol "Admin" sin restricción alguna.
// Todo esto viola principios de diseño seguro como separación de
// responsabilidades, mínimo privilegio y protección de configuraciones.
// ================================================================

namespace EjemploInseguroCapitulo3.Models
{
    /// <summary>
    /// Entidad de Usuario con estructura básica.
    /// En esta versión insegura, carece de validación de entrada
    /// y aplica valores por defecto inseguros.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// Generado o asignado sin validación.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// No tiene validación de longitud ni de formato.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Clave de acceso del usuario.
        /// Mala práctica: clave por defecto hardcodeada.
        /// No se almacena de forma segura ni se aplica hash.
        /// </summary>
        public string Clave { get; set; } = "12345";

        /// <summary>
        /// Rol del usuario dentro del sistema.
        /// Mala práctica: permite que cualquier usuario se declare Admin.
        /// No existe control real de autorización.
        /// </summary>
        public string Rol { get; set; } = "Admin";
    }
}
