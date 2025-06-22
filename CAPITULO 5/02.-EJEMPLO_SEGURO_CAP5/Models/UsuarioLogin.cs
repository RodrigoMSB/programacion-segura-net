// ================================================================
//   UsuarioLogin.cs — Modelo de dominio (Versión SEGURA)
// ================================================================
// Este archivo define la estructura de datos que recibe el servidor
// cuando un usuario envía sus credenciales para autenticarse.
//
// Forma parte del flujo seguro del Ejemplo SEGURO — Capítulo 5,
// donde se implementa autenticación robusta con emisión de JWT
// y control de acceso basado en roles.
// ================================================================

namespace EjemploSeguroCapitulo5.Models
{
    /// <summary>
    /// Representa el conjunto mínimo de campos necesarios
    /// para autenticar a un usuario:
    /// - Nombre de usuario.
    /// - Contraseña en texto claro (se recomienda aplicar hashing
    ///   y validación en un escenario real).
    /// 
    /// Este modelo es usado como parámetro de entrada en AuthController.
    /// </summary>
    public class UsuarioLogin
    {
        /// <summary>
        /// Nombre de usuario que intenta iniciar sesión.
        /// 
        /// En una aplicación real se recomienda validar longitud,
        /// formato y aplicar reglas de unicidad.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña ingresada por el usuario.
        /// 
        /// Importante: En este ejemplo se compara como texto plano,
        /// pero en producción siempre se debe aplicar hashing seguro
        /// y políticas de complejidad de contraseñas.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;
    }
}
