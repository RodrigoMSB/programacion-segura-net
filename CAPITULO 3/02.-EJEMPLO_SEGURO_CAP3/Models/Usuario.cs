// ================================================================
//   Usuario.cs — Modelo de dominio (Versión SEGURA)
// ================================================================
// Este archivo define la entidad Usuario para el ejemplo seguro del
// Capítulo 3. Aplica validación de campos mediante atributos de datos
// para garantizar la integridad de la información y prevenir entradas
// maliciosas. Además, se elimina el uso de claves hardcodeadas y se
// restringen los valores permitidos para el rol, reforzando el
// principio de mínimo privilegio.
// ================================================================

using System.ComponentModel.DataAnnotations;

namespace EjemploSeguroCapitulo3.Models
{
    /// <summary>
    /// Representa un usuario dentro del sistema con campos validados.
    /// Implementa validación declarativa para garantizar seguridad en
    /// la entrada de datos y coherencia con las reglas de negocio.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// Se asigna manual o automáticamente según la lógica de negocio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// Validación: obligatorio y longitud máxima de 100 caracteres.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Clave de acceso del usuario.
        /// Validación: obligatorio, mínimo 6 caracteres, máximo 100.
        /// No se almacena en texto plano en escenarios reales.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Clave { get; set; } = string.Empty;

        /// <summary>
        /// Rol asignado al usuario.
        /// Validación: solo se permiten 'Admin' o 'User'.
        /// Refuerza el principio de mínimo privilegio.
        /// </summary>
        [Required]
        [RegularExpression("Admin|User")]
        public string Rol { get; set; } = "User";
    }
}