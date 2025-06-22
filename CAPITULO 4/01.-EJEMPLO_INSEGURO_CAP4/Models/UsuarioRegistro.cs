// ================================================================
//   UsuarioRegistro.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Este archivo define la clase UsuarioRegistro para el ejemplo inseguro
// del Capítulo 4. Se utiliza para simular un formulario de registro
// sin aplicar ninguna validación ni restricción de formato.
//
// Esta implementación intencionalmente carece de:
// - Reglas de validación de campos.
// - Limitación de longitud.
// - Verificación de formato (correo, contraseña).
//
// Sirve como caso de estudio para contrastar con la versión segura
// que aplica Data Annotations y FluentValidation.
// ================================================================

namespace EjemploInseguroCapitulo4.Models
{
    /// <summary>
    /// Representa los datos básicos enviados por un usuario
    /// para registrarse. En esta versión, no se valida
    /// ninguno de los campos, permitiendo entradas
    /// mal formadas o incluso maliciosas.
    /// </summary>
    public class UsuarioRegistro
    {
        /// <summary>
        /// Nombre del usuario que intenta registrarse.
        /// No tiene restricción de caracteres, longitud
        /// ni control de inyección de scripts.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// No se valida si el formato es correcto,
        /// lo que permite entradas no conformes.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña proporcionada por el usuario.
        /// No tiene políticas de complejidad, longitud
        /// mínima ni cifrado, exponiendo riesgo de
        /// almacenamiento inseguro si se persistiera.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;
    }
}