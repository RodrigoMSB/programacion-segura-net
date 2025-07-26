// ====================================================================================================
// ExceptionHandlingMiddleware.cs — Middleware Global para el Manejo Centralizado de Excepciones
// ====================================================================================================
// Este middleware captura cualquier excepción no controlada que ocurra durante el procesamiento
// de solicitudes HTTP dentro del pipeline de ASP.NET Core.
//
// Su propósito es:
// - Evitar fugas de stack traces al cliente.
// - Garantizar una respuesta consistente en formato JSON ante errores.
// - Registrar detalles técnicos en los logs para posterior análisis.
//
// ----------------------------------------------------------------------------------------------------
// IMPORTS — Librerías necesarias para acceso al contexto HTTP, logging y serialización JSON
// ----------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;               // Acceso a contexto HTTP, respuestas y flujo de middleware.
using Microsoft.Extensions.Logging;            // Para registrar errores en el sistema de logs de ASP.NET Core.
using System.Net;                              // Para trabajar con códigos HTTP estándar.
using System.Text.Json;                        // Para convertir objetos a JSON.

// ----------------------------------------------------------------------------------------------------
// ESPACIO DE NOMBRES
// ----------------------------------------------------------------------------------------------------
namespace SeguridadBancoFinal.Middleware
{
    /// <summary>
    /// Middleware personalizado que intercepta excepciones no controladas,
    /// evitando que lleguen sin formato al cliente.
    /// 
    /// Implementa una política de manejo global de errores:
    /// - Registra el error en el log del sistema.
    /// - Retorna una respuesta estructurada con código HTTP 500.
    /// - Mantiene el sistema robusto frente a fallos internos.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        // =========================================================================================
        // DEPENDENCIAS INYECTADAS
        // =========================================================================================

        private readonly RequestDelegate _next;                         // Delegado que representa el siguiente middleware en el pipeline.
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;  // Logger especializado para esta clase.

        /// <summary>
        /// Constructor que permite inyectar el middleware siguiente y el sistema de logging.
        /// </summary>
        /// <param name="next">Siguiente componente del pipeline de ASP.NET.</param>
        /// <param name="logger">Instancia de logger para registrar errores.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // =========================================================================================
        // MÉTODO PRINCIPAL: Invoke
        // =========================================================================================

        /// <summary>
        /// Método invocado automáticamente por el pipeline de ASP.NET Core.
        /// Intenta ejecutar el siguiente middleware y captura cualquier excepción lanzada.
        /// </summary>
        /// <param name="context">Contexto actual de la solicitud HTTP.</param>
        /// <returns>Tarea asincrónica que representa la ejecución del middleware.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Delegar al siguiente middleware del pipeline.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Registrar la excepción con su stack trace completo.
                _logger.LogError(ex, "Excepción no controlada atrapada por el middleware.");

                // Generar una respuesta JSON al cliente.
                await HandleExceptionAsync(context, ex);
            }
        }

        // =========================================================================================
        // MÉTODO AUXILIAR: HandleExceptionAsync
        // =========================================================================================

        /// <summary>
        /// Construye y escribe la respuesta de error al cliente en formato JSON.
        /// También asigna el código de estado HTTP correspondiente (500).
        /// </summary>
        /// <param name="context">Contexto HTTP donde ocurrió la excepción.</param>
        /// <param name="exception">Excepción capturada por el middleware.</param>
        /// <returns>Tarea asincrónica para escribir la respuesta JSON.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Objeto anónimo con el mensaje a mostrar al cliente.
            var response = new
            {
                message = "Ha ocurrido un error inesperado. Por favor contacte a soporte.",
                detalle = exception.GetType().Name // Tipo de excepción (sin stack trace).
            };

            // Configurar cabecera de respuesta como JSON y código 500.
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Serializar y devolver el objeto como JSON.
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}