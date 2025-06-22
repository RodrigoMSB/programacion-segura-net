// ================================================================
// IUsuarioService.cs — Interfaz del servicio de usuario
// ================================================================
// Define el contrato que debe cumplir cualquier clase de servicio 
// encargada de gestionar operaciones relacionadas con usuarios.
// Aplica el principio de programación contra interfaces, no implementaciones.
// ================================================================

namespace EjemploSeguridadCapitulo1.Services
{
    /// <summary>
    /// Contrato base para operaciones de negocio relacionadas con usuarios.
    /// 
    /// Principios aplicados:
    /// - Desacopla la lógica de usuario de su implementación concreta.
    /// - Facilita la prueba unitaria mediante la inyección de dependencias.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Verifica si un usuario existe en el sistema a partir de su nombre de usuario.
        /// Se utiliza para validar registros duplicados o lógica de autenticación.
        /// </summary>
        /// <param name="username">Nombre de usuario a verificar.</param>
        /// <returns>True si existe; False en caso contrario.</returns>
        bool UsuarioExiste(string username);

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// La contraseña debe proporcionarse en texto plano, ya que se hashea dentro de la implementación del servicio.
        /// </summary>
        /// <param name="username">Nombre de usuario único.</param>
        /// <param name="email">Correo electrónico válido del usuario.</param>
        /// <param name="passwordPlano">Contraseña en texto plano (se convierte a hash).</param>
        void CrearUsuario(string username, string email, string passwordPlano);
    }
}