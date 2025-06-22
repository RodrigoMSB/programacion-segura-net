// ================================================================
//   ErrorController.cs — Controlador (Versión SEGURO)
// ================================================================
// Este controlador implementa un manejo de errores robusto y centralizado,
// cumpliendo con las prácticas seguras:
//
// ✔ Captura todas las excepciones no controladas mediante el middleware UseExceptionHandler.
// ✔ Registra los detalles internos en logs usando ILogger.
// ✔ Devuelve al cliente un mensaje neutro y estructurado con ProblemDetails
//   siguiendo el estándar RFC 7807.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres necesarios
// ---------------------------------------------------------------

// Importa las interfaces para acceder a la característica de excepción global.
using Microsoft.AspNetCore.Diagnostics;

// Importa la infraestructura base para definir controladores RESTful.
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo8.Controllers
{
    /// <summary>
    /// Controlador dedicado a manejar errores globalmente.
    ///
    /// Ruta especial: /error
    ///
    /// Este controlador se activa automáticamente cuando ocurre
    /// una excepción no controlada y `UseExceptionHandler` la redirige aquí.
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Dependencia para registrar logs estructurados de errores.
        /// </summary>
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// Constructor que recibe la implementación de ILogger.
        /// </summary>
        /// <param name="logger">Logger inyectado para registrar errores.</param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Endpoint que maneja la excepción capturada globalmente.
        ///
        /// - Extrae la excepción original usando IExceptionHandlerFeature.
        /// - Registra el error junto con el TraceId de la solicitud.
        /// - Devuelve un objeto ProblemDetails con un mensaje genérico.
        ///
        /// Ruta: /error
        /// </summary>
        /// <returns>Respuesta HTTP 500 con ProblemDetails neutro.</returns>
        [Route("error")]
        public IActionResult HandleError()
        {
            // -----------------------------------------------------------
            // Obtiene la excepción original del contexto de la petición.
            // -----------------------------------------------------------
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            // Identificador único de la solicitud, útil para correlación de logs.
            var traceId = HttpContext.TraceIdentifier;

            // -----------------------------------------------------------
            // Registra el error de forma estructurada con ILogger.
            // El TraceId ayuda a los equipos de soporte a rastrear el incidente.
            // -----------------------------------------------------------
            _logger.LogError(exception, "Excepción no controlada. TraceId: {TraceId}", traceId);

            // -----------------------------------------------------------
            // Construye un objeto ProblemDetails para la respuesta,
            // siguiendo el estándar RFC 7807 para APIs REST.
            // -----------------------------------------------------------
            var problem = new ProblemDetails
            {
                Title = "Ocurrió un error inesperado.",
                Status = 500,
                Detail = $"Contacte a soporte con el siguiente código: {traceId}"
            };

            // Devuelve respuesta HTTP 500 con ProblemDetails.
            return StatusCode(500, problem);
        }
    }
}