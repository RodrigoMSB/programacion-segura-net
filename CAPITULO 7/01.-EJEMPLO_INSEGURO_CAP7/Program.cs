// ================================================================
//   Program.cs — Configuración Principal (Versión INSEGURA)
// ================================================================
// Este archivo configura la aplicación ASP.NET Core intencionalmente
// con malas prácticas de manejo de sesiones.
//
// Aspectos inseguros destacados:
// - Sesiones sin expiración razonable (365 días).
// - Cookies de sesión sin protección: HttpOnly, Secure, SameSite desactivados.
// - No hay redirección HTTPS ni políticas HSTS.
// ================================================================

// ---------------------------------------------------------------
// Crear el builder para configurar servicios y middleware.
// ---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRAR SERVICIOS NECESARIOS PARA SESSION
// ---------------------------------------------------------------

// ✅ Necesario para que el middleware Session funcione.
// Provee almacenamiento de datos de sesión en memoria.
// ❌ En producción se recomienda usar un cache distribuido real
// como Redis o SQL Server, no memoria local.
builder.Services.AddDistributedMemoryCache();

// ---------------------------------------------------------------
// CONFIGURAR SESIÓN (INSEGURA)
// ---------------------------------------------------------------

builder.Services.AddSession(options =>
{
    // ❌ Tiempo de expiración excesivo: 365 días.
    // La sesión debería tener un IdleTimeout corto (15–30 min).
    options.IdleTimeout = TimeSpan.FromDays(365);

    // ❌ HttpOnly deshabilitado: la cookie es accesible por JavaScript.
    // Permite ataques XSS para robar la cookie de sesión.
    options.Cookie.HttpOnly = false;

    // ❌ SecurePolicy = None: la cookie se envía en HTTP no cifrado.
    // Riesgo de sniffing en redes inseguras.
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;

    // ❌ SameSite = None: permite envío de la cookie en peticiones cross-site.
    // Facilita ataques CSRF.
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

// ---------------------------------------------------------------
// REGISTRAR CONTROLADORES
// ---------------------------------------------------------------

builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUIR Y CONFIGURAR PIPELINE HTTP
// ---------------------------------------------------------------

var app = builder.Build();

// ✅ Registrar middleware de Session (aunque su configuración sea insegura)
app.UseSession();

// Registrar rutas de los controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();