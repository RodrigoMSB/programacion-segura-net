// ================================================================
//   UsuarioRegistro.cs — Modelo de dominio (Versión SEGURA)
// ================================================================
// Este archivo define el modelo de datos para el formulario de registro
// del Ejemplo SEGURO — Capítulo 4. Aplica validación robusta mediante
// atributos de Data Annotations para asegurar que cada campo cumpla
// con reglas de formato, longitud y obligatoriedad.
//
// Es complementado por un validador FluentValidation para reglas más
// complejas, siguiendo el principio de validación múltiple.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

// Importa el espacio de nombres que contiene los atributos de
// validación de datos: Required, StringLength, EmailAddress, etc.
using System.ComponentModel.DataAnnotations;

namespace EjemploSeguroCapitulo4.Models
{
    /// <summary>
    /// Representa los datos que un usuario debe enviar para
    /// registrarse en el sistema. Este modelo aplica reglas de
    /// validación básica mediante atributos declarativos.
    /// </summary>
    public class UsuarioRegistro
    {
        /// <summary>
        /// Nombre del usuario.
        /// 
        /// Validación:
        /// - [Required]: Obligatorio, no puede estar vacío.
        /// - [StringLength]: Debe tener entre 3 y 50 caracteres.
        /// 
        /// Estas reglas aseguran que se envíe un nombre con una
        /// longitud coherente, previniendo entradas vacías o excesivas.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// 
        /// Validación:
        /// - [Required]: Campo obligatorio.
        /// - [EmailAddress]: Verifica que el formato corresponda a un email válido.
        /// 
        /// Estas restricciones evitan que se envíen direcciones mal formadas
        /// o campos vacíos.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// 
        /// Validación:
        /// - [Required]: Campo obligatorio.
        /// - [StringLength]: Debe tener entre 8 y 100 caracteres.
        /// 
        /// Nota: La complejidad de la contraseña (mayúsculas, números)
        /// se refuerza mediante FluentValidation para mantener separado
        /// el control de lógica avanzada.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Contrasena { get; set; } = string.Empty;
    }
}