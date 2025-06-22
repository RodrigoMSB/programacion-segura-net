// ================================================================
//   UsuarioService.cs — Lógica de negocio (Versión SEGURA)
// ================================================================
// Este archivo define la capa de servicio para la gestión de usuarios
// en la versión segura del Capítulo 3. Implementa:
// - Separación de responsabilidades: encapsula toda la lógica de negocio.
// - Control de acceso basado en roles para operaciones sensibles.
// - Simulación de manejo de secretos utilizando variables de entorno.
// Esta estructura refuerza principios como mínimo privilegio y protección
// de configuración, alineados con prácticas modernas de seguridad.
// ================================================================

using EjemploSeguroCapitulo3.Models;

namespace EjemploSeguroCapitulo3.Services
{
    /// <summary>
    /// Servicio dedicado a operaciones de usuarios.
    /// Encapsula la lógica de creación, consulta y obtención de secretos,
    /// manteniendo el controlador limpio y desacoplado.
    /// </summary>
    public class UsuarioService
    {
        /// <summary>
        /// Lista interna que simula almacenamiento persistente.
        /// En escenarios reales, se sustituye por un repositorio o base de datos.
        /// </summary>
        private readonly List<Usuario> _usuarios = new();

        /// <summary>
        /// Variable que representa un secreto gestionado externamente.
        /// Se obtiene desde variable de entorno para evitar hardcodeo.
        /// </summary>
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "SECRET_SIMULADO";

        /// <summary>
        /// Agrega un nuevo usuario a la lista interna.
        /// Asume que los datos ya fueron validados en el controlador.
        /// </summary>
        /// <param name="usuario">Instancia de Usuario validada.</param>
        public void CrearUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }

        /// <summary>
        /// Devuelve todos los usuarios almacenados.
        /// </summary>
        /// <returns>Lista de usuarios registrados.</returns>
        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarios;
        }

        /// <summary>
        /// Devuelve el API Key protegido.
        /// Solo accesible para usuarios con rol 'Admin'.
        /// Lanza excepción si el rol no está autorizado.
        /// </summary>
        /// <param name="rol">Rol del solicitante.</param>
        /// <returns>Secreto de API.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Se lanza si el rol no es 'Admin'.
        /// </exception>
        public string ObtenerApiKey(string rol)
        {
            if (rol != "Admin")
                throw new UnauthorizedAccessException("No autorizado: se requiere rol Admin.");
            return _apiKey;
        }
    }
}