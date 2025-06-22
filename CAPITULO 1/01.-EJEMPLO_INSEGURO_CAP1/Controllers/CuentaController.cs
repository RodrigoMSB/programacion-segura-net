// ================================================================
//   CuentaController.cs — Controlador de cuenta (Versión INSEGURA)
// ================================================================
// Este archivo define el controlador de operaciones básicas de cuenta:
// registro de usuario, inicio de sesión y consulta de perfil.
// Esta versión NO aplica prácticas de seguridad recomendadas.
// Sirve exclusivamente como ejemplo de malas prácticas.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------

using EjemploInseguroCapitulo1.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo1.Controllers
{
    /// <summary>
    /// Controlador de cuenta para el ejemplo INSEGURO.
    /// Expone endpoints básicos sin validación ni autenticación real.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : ControllerBase
    {
        // -----------------------------------------------------------
        // DEPENDENCIAS
        // -----------------------------------------------------------

        /// <summary>
        /// Contexto de base de datos en memoria.
        /// No implementa patrón repositorio ni capa de servicio.
        /// Se usa directamente desde el controlador.
        /// </summary>
        private readonly ContextoBaseDatos _contexto;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="contexto">Instancia de ContextoBaseDatos.</param>
        public CuentaController(ContextoBaseDatos contexto)
        {
            _contexto = contexto;
        }

        // -----------------------------------------------------------
        // ENDPOINT: Registrar usuario
        // -----------------------------------------------------------

        /// <summary>
        /// Registra un nuevo usuario.
        /// No valida datos de entrada ni verifica duplicados.
        /// Guarda la contraseña en texto plano.
        /// </summary>
        /// <param name="usuario">Objeto Usuario recibido desde el cliente.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPost("registrar")]
        public IActionResult Registrar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
            return Ok("Usuario registrado (INSEGURO)");
        }

        // -----------------------------------------------------------
        // ENDPOINT: Iniciar sesión
        // -----------------------------------------------------------

        /// <summary>
        /// Inicia sesión validando credenciales tal cual fueron ingresadas.
        /// Comparación de contraseña en texto plano.
        /// No se generan tokens ni sesiones.
        /// </summary>
        /// <param name="credenciales">Usuario con nombre y contraseña.</param>
        /// <returns>Mensaje de bienvenida o error de autenticación.</returns>
        [HttpPost("iniciar-sesion")]
        public IActionResult IniciarSesion([FromBody] Usuario credenciales)
        {
            var usuario = _contexto.Usuarios.FirstOrDefault(u =>
                u.NombreUsuario == credenciales.NombreUsuario && u.Contrasena == credenciales.Contrasena);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas (INSEGURO)");

            return Ok($"Bienvenido {usuario.NombreUsuario}, su rol es {usuario.Rol} (sin seguridad real!)");
        }

        // -----------------------------------------------------------
        // ENDPOINT: Perfil
        // -----------------------------------------------------------

        /// <summary>
        /// Devuelve información básica de perfil.
        /// No tiene ninguna restricción de acceso.
        /// Cualquiera puede consultar esta ruta sin autenticación.
        /// </summary>
        /// <returns>Mensaje indicando que la ruta es pública.</returns>
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            return Ok("Esto debería estar protegido, pero es PÚBLICO en esta versión insegura.");
        }
    }
}