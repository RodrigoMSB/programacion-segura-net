using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace SeguridadBancoFinal.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // ================================
        // Invocado por el pipeline de ASP.NET
        // ================================
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada atrapada por el middleware.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // ================================
        // Manejo de excepción y respuesta
        // ================================
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new
            {
                message = "Ha ocurrido un error inesperado. Por favor contacte a soporte.",
                detalle = exception.GetType().Name
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}