// ================================================================
//   Program.cs — Configuración Principal (Versión SEGURO)
// ================================================================
// Configura una aplicación ASP.NET Core con prácticas seguras
// para el manejo de sesiones.
//
// ✔ Cookies de sesión protegidas con HttpOnly, Secure, SameSite.
// ✔ Expiración controlada y razonable.
// ✔ HTTPS Redirection y HSTS habilitados.
// ✔ Nombre de cookie personalizado para mayor claridad.
// ================================================================

// ---------------------------------------------------------------
// Crear el builder para registrar servicios y middleware.
// ---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRAR BACKEND DE CACHE PARA SESIÓN
// ---------------------------------------------------------------
// Agrega almacenamiento en memoria para datos de sesión.
// Para producción se recomienda un cache distribuido (Redis, SQL Server, etc.)
builder.Services.AddDistributedMemoryCache();

// ---------------------------------------------------------------
// CONFIGURAR SESIÓN DE FORMA SEGURA
// ---------------------------------------------------------------
builder.Services.AddSession(options =>
{
    // ✅ Tiempo de expiración corto: 20 minutos de inactividad.
    // Evita sesiones indefinidas.
    options.IdleTimeout = TimeSpan.FromMinutes(20);

    // ✅ HttpOnly: impide acceso por scripts JS. Protege contra XSS.
    options.Cookie.HttpOnly = true;

    // ✅ SecurePolicy Always: la cookie solo viaja por HTTPS.
    // Asegura confidencialidad en tránsito.
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;

    // ✅ SameSite Strict: impide envío de cookie en peticiones cross-site.
    // Reduce el riesgo de CSRF.
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;

    // ✅ Nombre personalizado de cookie: mejora rastreabilidad y claridad.
    options.Cookie.Name = ".MiAppSesionSegura";
});

// ---------------------------------------------------------------
// REGISTRAR CONTROLADORES
// ---------------------------------------------------------------
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUIR Y CONFIGURAR PIPELINE HTTP
// ---------------------------------------------------------------
var app = builder.Build();

// ✅ Habilitar HSTS: obliga HTTPS y protege contra downgrade attacks.
app.UseHsts();

// ✅ Redirigir automáticamente peticiones HTTP a HTTPS.
app.UseHttpsRedirection();

// ✅ Middleware de sesiones: debe ir antes de MapControllers.
app.UseSession();

// Registrar rutas de los controladores.
app.MapControllers();

// Ejecutar la aplicación.
app.Run();