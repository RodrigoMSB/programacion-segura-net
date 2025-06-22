// ================================================================
//   Usuario.cs — Modelo de dominio
// ================================================================
// Este archivo define la entidad de usuario del sistema.
// Incluye validaciones y anotaciones de datos para reforzar la seguridad
// desde la capa de entrada, siguiendo principios de arquitectura limpia.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------

// Importa los atributos de validación de datos:
// [Required], [StringLength], [EmailAddress], etc.
// Estas anotaciones permiten validar la entrada en tiempo de ejecución
// y proteger la integridad del modelo.
using System.ComponentModel.DataAnnotations;

namespace EjemploSeguridadCapitulo1.Models
{
    /// <summary>
    /// Entidad de dominio que representa a un usuario registrado en el sistema.
    /// Incluye campos críticos para autenticación, autorización y control de acceso.
    /// 
    /// Seguridad aplicada:
    /// - Validaciones de datos para prevenir inyecciones y datos inconsistentes.
    /// - Contraseña almacenada como hash, no en texto plano.
    /// - Rol definido para control de acceso basado en privilegios.
    /// 
    /// Relación con la arquitectura:
    /// Este modelo es parte del núcleo de negocio: es agnóstico a la base de datos 
    /// y a la capa de infraestructura.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario en la base de datos.
        /// Se genera automáticamente por el sistema (por EF Core).
        /// 
        /// Buenas prácticas:
        /// No requiere validación manual, pues el ORM lo maneja.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario, usado para autenticación.
        /// 
        /// Validaciones:
        /// - [Required]: Obliga a que siempre exista valor.
        /// - [StringLength]: Define largo mínimo y máximo.
        /// 
        /// Propósito de seguridad:
        /// Limitar la longitud previene exploits de buffer overflow y spam de input.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre de usuario debe tener entre 4 y 50 caracteres.")]
        public string Username { get; set; }

        /// <summary>
        /// Correo electrónico asociado al usuario.
        /// 
        /// Validaciones:
        /// - [Required]: No puede ser nulo.
        /// - [EmailAddress]: Verifica formato válido.
        /// 
        /// Seguridad:
        /// El correo puede usarse para phishing, por lo que se valida server-side.
        /// Se recomienda no exponerlo públicamente.
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe proporcionar un correo válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Contraseña del usuario, almacenada como hash seguro.
        /// 
        /// Seguridad:
        /// - No se almacena texto plano.
        /// - Se genera hash con PBKDF2/bcrypt/Argon2 en la capa de servicio.
        /// 
        /// Nota:
        /// No se aplican validaciones de formato aquí, 
        /// ya que se supone que llega procesada.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Rol del usuario dentro de la aplicación.
        /// Define permisos y restricciones de acceso.
        /// 
        /// Seguridad:
        /// - Permite políticas de control de acceso basadas en roles.
        /// - Se valida siempre desde backend para evitar manipulaciones.
        /// </summary>
        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; }
    }
}