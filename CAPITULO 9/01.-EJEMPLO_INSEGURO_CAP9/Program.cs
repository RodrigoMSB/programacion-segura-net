// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Configura la aplicación MVC básica sin medidas de seguridad modernas:
// - No aplica políticas de cookies seguras.
// - No usa antiforgery ni validación CSRF.
// - No configura HTTPS obligatorio.
// - No registra servicios de sesión ni autenticación real.
//
// Esta configuración sirve como contraste didáctico para la versión segura.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Librerías fundamentales para levantar un WebApplication.
// ---------------------------------------------------------------

// Proporciona los componentes y middleware para construir la tubería HTTP.
using Microsoft.AspNetCore.Builder;

// Permite registrar y gestionar los servicios de la aplicación,
// como MVC, sesiones, inyección de dependencias, etc.
using Microsoft.Extensions.DependencyInjection;

// Contiene utilidades para detectar el entorno de ejecución (Development, Production, etc.)
// y configurar la app en consecuencia.
using Microsoft.Extensions.Hosting;

// ---------------------------------------------------------------
// Crear builder para configurar servicios.
// ---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Registrar servicios para MVC con soporte para controladores y vistas.
// Sin validación de antiforgery ni protección adicional.
// ---------------------------------------------------------------
builder.Services.AddControllersWithViews();

// ---------------------------------------------------------------
// Construir la aplicación.
// ---------------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------------
// Permitir servir archivos estáticos (CSS, JS, imágenes).
// ---------------------------------------------------------------
app.UseStaticFiles();

// ---------------------------------------------------------------
// Configurar el enrutamiento por convención.
// Controlador por defecto: Account
// Acción por defecto: Register
// ---------------------------------------------------------------
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}"
);

// ---------------------------------------------------------------
// Ejecutar la aplicación.
// ---------------------------------------------------------------
app.Run();