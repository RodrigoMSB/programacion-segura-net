// ================================================================
//   UsuarioController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este archivo define el controlador principal para el ejemplo
// inseguro del Capítulo 3. Contiene múltiples malas prácticas:
// - Lógica de negocio mezclada directamente con controladores.
// - No se valida la entrada ni se protege la clave del usuario.
// - No se aplican roles, claims ni autenticación.
// - Se expone un secreto de API en texto plano.
// Todo esto ilustra un diseño y arquitectura sin principios de
// seguridad como separación de responsabilidades y mínimo privilegio.
// ================================================================

using EjemploInseguroCapitulo3.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo3.Controllers
{
    /// <summary>
    /// Controlador para gestión de usuarios.
    /// En esta versión insegura, mezcla lógica de negocio,
    /// acceso a datos y expone información sensible.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Lista estática de usuarios simulando almacenamiento en memoria.
        /// No se aplica persistencia segura ni control de acceso.
        /// </summary>
        private static List<Usuario> _usuarios = new List<Usuario>();

        /// <summary>
        /// Crea un usuario nuevo.
        /// No se valida la entrada, se permite cualquier rol y clave insegura.
        /// </summary>
        /// <param name="usuario">Objeto Usuario recibido desde el cliente.</param>
        /// <returns>Confirmación insegura.</returns>
        [HttpPost("crear")]
        public IActionResult Crear(Usuario usuario)
        {
            // No se aplica validación de modelo ni hash de clave
            _usuarios.Add(usuario);
            return Ok("Usuario creado sin validación ni seguridad.");
        }

        /// <summary>
        /// Devuelve todos los usuarios registrados.
        /// No requiere autenticación ni verificación de permisos.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        [HttpGet("todos")]
        public IActionResult ObtenerTodos()
        {
            // No existe control de acceso
            return Ok(_usuarios);
        }

        /// <summary>
        /// Devuelve un secreto de API hardcodeado.
        /// Mala práctica: expone información crítica en texto plano.
        /// </summary>
        /// <returns>Secreto expuesto públicamente.</returns>
        [HttpGet("secreto")]
        public IActionResult ObtenerSecreto()
        {
            string secreto = "API_KEY_SUPER_SECRETA_123";
            return Ok($"El secreto es: {secreto}");
        }
    }
}