// ================================================================
//   Usuario.cs — Entidad de Dominio
// ================================================================
// Define la estructura base de un usuario en la aplicación.
// Contiene las propiedades públicas que serán serializadas/deserializadas
// por la API REST para registrar y autenticar usuarios.
// ================================================================

namespace EjemploSeguroCapitulo9.Models
{
    /// <summary>
    /// Entidad simple que representa un usuario con nombre y contraseña.
    /// ⚠️ Nota: la propiedad Password siempre se manipula con hash + salt
    /// en la lógica de negocio, nunca se almacena en texto plano en producción.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nombre de usuario elegido por el cliente.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña en texto plano (solo se usa en la solicitud).
        /// Internamente se convierte a hash + salt.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}