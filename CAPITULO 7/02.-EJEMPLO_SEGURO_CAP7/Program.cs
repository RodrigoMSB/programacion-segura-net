// ============================================================================
// Program.cs — Configuración Principal (Versión Segura y Profesional)
// ============================================================================
// Este archivo configura una aplicación ASP.NET Core con prácticas avanzadas
// de seguridad para el manejo de sesiones y cookies.
//
// Incluye:
// - Cookies de sesión seguras (HttpOnly, Secure, SameSite, IsEssential, MaxAge).
// - Expiración controlada y explícita de sesión.
// - Redirección a HTTPS y HSTS habilitado.
// - Logging básico para trazabilidad de sesiones activas.
// - Middleware para mostrar uso de sesión.
// - Configuración de AntiForgery para prevención de CSRF.
// - Ejemplo comentado de Redis para almacenamiento distribuido.
//
// Uso esperado:
// - Entornos bancarios o de alta sensibilidad.
// - Demostración de prácticas de seguridad sólidas.
// ============================================================================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURACIÓN DE LOGGING
// ----------------------------------------------------------------------------
// Se limpia cualquier proveedor anterior y se agrega consola.
// Esto permite tener trazabilidad básica de lo que ocurre en la aplicación.
// ============================================================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ============================================================================
// CONFIGURACIÓN DEL ALMACENAMIENTO DE SESIONES
// ----------------------------------------------------------------------------
// En esta sección se define dónde se almacenan los datos de sesión.
// Para desarrollo se usa la memoria local, pero en producción
// se recomienda un almacenamiento distribuido como Redis.
// ============================================================================

// Uso de memoria en desarrollo:
builder.Services.AddDistributedMemoryCache();

// Uso de Redis en producción (comentado como ejemplo):
// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = "redis-endpoint:6379,password=secret";
// });

// ============================================================================
// CONFIGURACIÓN DE LA SESIÓN HTTP
// ----------------------------------------------------------------------------
// Se definen parámetros estrictos para las cookies de sesión.
// Incluye prácticas recomendadas como:
// - Tiempo de expiración limitado.
// - SecurePolicy Always para requerir HTTPS.
// - SameSite Strict para protección contra CSRF.
// - HttpOnly para proteger de XSS.
// - IsEssential para cumplir regulaciones como RGPD/GDPR.
// - MaxAge explícito para claridad en cliente.
// ============================================================================
builder.Services.AddSession(options =>
{
    // Tiempo de inactividad máximo antes de caducar la sesión.
    options.IdleTimeout = TimeSpan.FromMinutes(20);

    // Marcar cookie como solo accesible vía HTTP, no por scripts.
    options.Cookie.HttpOnly = true;

    // Exigir siempre HTTPS para la cookie.
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    // SameSite Strict evita que la cookie sea enviada en requests cross-site.
    options.Cookie.SameSite = SameSiteMode.Strict;

    // Nombre personalizado para identificación clara en inspección.
    options.Cookie.Name = ".MiAppSesionSegura";

    // Marcar la cookie como esencial para RGPD/GDPR.
    // Evita problemas legales al no requerir consentimiento explícito.
    options.Cookie.IsEssential = true;

    // MaxAge explícito para definir cuánto vive la cookie en cliente.
    // Opcional pero recomendado para trazabilidad clara.
    options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
});

// ============================================================================
// CONFIGURACIÓN DE ANTIFORGERY
// ----------------------------------------------------------------------------
// Provee protección contra CSRF al emitir y validar tokens anti-forgery.
// La cookie se define con atributos estrictos de seguridad.
// ============================================================================
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = ".MiAppAntiCSRF";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// ============================================================================
// REGISTRAR CONTROLADORES
// ----------------------------------------------------------------------------
// Incluye el soporte para controladores y rutas de API.
// ============================================================================
builder.Services.AddControllers();

// ============================================================================
// CONSTRUIR LA APP
// ----------------------------------------------------------------------------
// Se instancia la aplicación web con los servicios y middlewares configurados.
// ============================================================================
var app = builder.Build();

// ============================================================================
// CONFIGURACIÓN DE SEGURIDAD DE TRANSPORTE
// ----------------------------------------------------------------------------
// HSTS: fuerza al navegador a usar HTTPS siempre que sea posible.
// HTTPS Redirection: redirige solicitudes HTTP a HTTPS.
// ============================================================================
app.UseHsts();
app.UseHttpsRedirection();

// ============================================================================
// MIDDLEWARE DE SESIÓN
// ----------------------------------------------------------------------------
// ¡MUY IMPORTANTE!
// Debe declararse ANTES de cualquier middleware que use context.Session.
// Esto evita el error "Session has not been configured for this application or request."
// ============================================================================
app.UseSession();

// ============================================================================
// MIDDLEWARE PERSONALIZADO PARA LOGGING DE SESIONES
// ----------------------------------------------------------------------------
// Este middleware muestra cómo registrar en el log el uso de sesión.
// Permite a los equipos de seguridad auditar la existencia de sesiones activas.
// Es muy útil en contextos bancarios para monitorear actividad.
// ============================================================================
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    if (context.Session.IsAvailable)
    {
        logger.LogInformation("Sesión activa detectada. SessionID: {SessionId}, RequestPath: {RequestPath}",
            context.Session.Id, context.Request.GetDisplayUrl());
    }
    else
    {
        logger.LogInformation("Sesión NO disponible para esta solicitud. Path: {RequestPath}",
            context.Request.GetDisplayUrl());
    }

    await next();
});

// ============================================================================
// MAPEADO DE CONTROLADORES
// ----------------------------------------------------------------------------
// Asocia los controladores al pipeline de solicitudes HTTP.
// ============================================================================
app.MapControllers();

// ============================================================================
// INICIO DE LA APLICACIÓN
// ----------------------------------------------------------------------------
// Inicia el servidor web y empieza a procesar solicitudes.
// ============================================================================
app.Run();