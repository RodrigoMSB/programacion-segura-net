// ================================================================
//   PruebaController.cs — Controlador de prueba (Versión SEGURO)
// ================================================================
// Este controlador sirve para probar la configuración del manejo
// de errores centralizado:
//
// ✔ Lanza una excepción intencionalmente.
// ✔ El middleware global `UseExceptionHandler` la captura.
// ✔ Se registra internamente y se devuelve una respuesta neutra
//   al cliente usando ProblemDetails.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres necesarios
// ---------------------------------------------------------------

// Importa la base de ASP.NET Core para definir un controlador RESTful.
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo8.Controllers
{
    /// <summary>
    /// Controlador de prueba que expone un endpoint para provocar
    /// una excepción. Este error es capturado globalmente por el
    /// middleware `UseExceptionHandler` configurado en `Program.cs`.
    ///
    /// Ruta base: /prueba
    /// </summary>
    [ApiController]
    [Route("prueba")]
    public class PruebaController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP GET que lanza una excepción intencional.
        /// 
        /// ✅ Propósito:
        /// - Simular un fallo en tiempo de ejecución.
        /// - Verificar que la configuración segura capture el error,
        ///   lo registre con ILogger y devuelva un mensaje genérico
        ///   sin detalles internos.
        /// 
        /// Ruta: /prueba/provocar
        /// </summary>
        /// <returns>Lanza una excepción no controlada.</returns>
        [HttpGet("provocar")]
        public IActionResult ProvocarError()
        {
            // -----------------------------------------------------------
            // ❌ Excepción lanzada a propósito.
            // ✅ Será capturada por `UseExceptionHandler` y registrada.
            // ✅ El cliente solo verá un mensaje neutro estructurado.
            // -----------------------------------------------------------
            throw new Exception("Este error será capturado y manejado de forma segura.");
        }
    }
}