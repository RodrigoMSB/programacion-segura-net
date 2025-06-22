// ================================================================
//   Program.cs — Configuración Principal (Versión INSEGURA)
// ================================================================
// Este archivo muestra cómo una aplicación ASP.NET Core puede estar
// mal configurada para manejar errores:
//
// ❌ No usa un middleware global (`UseExceptionHandler`) para capturar
//    excepciones no controladas.
// ❌ No implementa `DeveloperExceptionPage` de forma controlada.
// ❌ No configura ningún sistema de logging.
// ❌ Permite que stack traces y detalles internos se propaguen al cliente.
//
// Propósito: demostrar las consecuencias de no aplicar un manejo
// centralizado de errores ni prácticas de seguridad básicas.
// ================================================================

// ---------------------------------------------------------------
// Crear y configurar el builder de la aplicación.
// ---------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Registrar servicios de controladores.
// ---------------------------------------------------------------
// Nota: No se registran middlewares adicionales de seguridad ni logging estructurado.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// Construir la aplicación.
// ---------------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------------
// ❌ MALA PRÁCTICA: no se usa UseExceptionHandler ni DeveloperExceptionPage.
//
// Esto implica que cualquier excepción no capturada se propagará tal cual,
// mostrando stack trace y detalles internos directamente en la respuesta HTTP.
// ---------------------------------------------------------------

// Mapeo de rutas de controladores (único middleware configurado).
app.MapControllers();

// Iniciar la aplicación web.
app.Run();