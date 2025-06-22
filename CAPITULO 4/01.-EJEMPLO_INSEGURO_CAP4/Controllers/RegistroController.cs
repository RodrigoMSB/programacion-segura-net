// ================================================================
//   RegistroController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este archivo define el controlador para simular un formulario
// de registro de usuario sin aplicar validación de entrada,
// sanitización de campos ni control de errores.
//
// Esta versión insegura muestra explícitamente malas prácticas:
//
// - No valida el modelo con ModelState.
// - No aplica reglas de Data Annotations ni FluentValidation.
// - No controla o escapa datos antes de devolverlos al cliente.
// - Acepta y procesa cualquier entrada, incluyendo scripts o payloads maliciosos.
//
// Sirve como referencia para contrastar con la versión segura.
// ================================================================

using EjemploInseguroCapitulo4.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo4.Controllers
{
    /// <summary>
    /// Controlador que simula el procesamiento de un formulario
    /// de registro de usuario. En esta versión insegura,
    /// no se aplica ninguna validación de entrada ni manejo
    /// de errores, exponiendo la aplicación a vulnerabilidades.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegistroController : ControllerBase
    {
        /// <summary>
        /// Endpoint POST que recibe los datos de registro
        /// enviados por el cliente. No se valida el modelo,
        /// no se filtran scripts ni entradas maliciosas,
        /// y se devuelve una respuesta usando los mismos datos recibidos.
        /// </summary>
        /// <param name="usuario">
        /// Objeto UsuarioRegistro recibido en el cuerpo de la solicitud.
        /// </param>
        /// <returns>
        /// Respuesta HTTP 200 OK confirmando el registro,
        /// sin verificar la integridad o legitimidad de los datos.
        /// </returns>
        [HttpPost("crear")]
        public IActionResult Crear([FromBody] UsuarioRegistro usuario)
        {
            // No valida ModelState ni aplica reglas de seguridad.
            // La entrada se devuelve tal como se recibió,
            // lo que puede exponer la aplicación a XSS o inyección.
            return Ok($"Registro exitoso para {usuario.Nombre} con correo {usuario.Email}.");
        }
    }
}