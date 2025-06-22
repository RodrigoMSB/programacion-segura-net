// ================================================================
//   Usuario.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Este archivo define el modelo de datos para simular usuarios
// en el Ejemplo Inseguro — Capítulo 5.
//
// Exhibe varias malas prácticas intencionales:
// - No valida credenciales ni formato de campos.
// - No aplica hashing a la contraseña; se guarda como texto plano.
// - El rol puede ser definido arbitrariamente por el cliente.
// 
// Este diseño ilustra cómo una implementación deficiente de autenticación
// expone la aplicación a escalación de privilegios y acceso no autorizado.
// ================================================================

namespace EjemploInseguroCapitulo5.Models
{
    /// <summary>
    /// Representa la información mínima de un usuario para
    /// simular autenticación y autorización.
    /// En esta versión insegura, no se aplica ninguna restricción,
    /// lo que permite crear usuarios con credenciales débiles
    /// y roles elevados arbitrariamente.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre de usuario.
        /// No se valida longitud, caracteres permitidos ni unicidad.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// En esta versión insegura, se almacena como texto plano,
        /// sin hashing ni políticas de complejidad.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario.
        /// Por defecto es 'Admin', lo que permite a cualquier cliente
        /// declararse administrador sin verificación del lado servidor.
        /// </summary>
        public string Rol { get; set; } = "Admin";
    }
}
