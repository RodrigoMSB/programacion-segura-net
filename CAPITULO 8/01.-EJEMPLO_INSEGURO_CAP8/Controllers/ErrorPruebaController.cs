// ================================================================
//   ErrorPruebaController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este controlador ilustra explícitamente una mala práctica en el manejo de errores:
//
// ✔ No se captura ninguna excepción de forma controlada.
// ✔ Se permite que el stack trace y el mensaje de la excepción lleguen directamente al cliente.
// ✔ No existe ningún mecanismo de logging ni redirección a un handler centralizado.
//
// Su propósito es servir como contraste con la versión segura implementada con
// middleware y manejo global de excepciones.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

// Importa la infraestructura base para definir controladores RESTful.
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo8.Controllers
{
    /// <summary>
    /// Controlador que expone un endpoint intencionalmente inseguro.
    /// 
    /// Ruta base: /errorprueba
    /// 
    /// Permite demostrar los riesgos de exponer excepciones sin manejo centralizado:
    /// - Divulgación de detalles internos.
    /// - Falta de logging estructurado.
    /// - Falta de separación de ambiente (Development vs Production).
    /// </summary>
    [ApiController]
    [Route("errorprueba")]
    public class ErrorPruebaController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP GET que genera y propaga una excepción sin capturarla.
        /// 
        /// Esta mala práctica permite que el framework devuelva un stack trace
        /// completo directamente al cliente, exponiendo información interna
        /// del servidor, clases y rutas de archivo.
        /// </summary>
        /// <returns>Una excepción no controlada que interrumpe la ejecución normal.</returns>
        [HttpGet("provocar")]
        public IActionResult ProvocarError()
        {
            // -----------------------------------------------------------
            // ❌ MAL: se lanza una excepción intencional SIN bloque try/catch.
            // ❌ MAL: no se delega al middleware global de manejo de errores.
            // ❌ MAL: el cliente recibe un detalle interno innecesario.
            // -----------------------------------------------------------
            throw new Exception("Error inseguro lanzado a propósito: detalle interno visible para el cliente.");
        }
    }
}