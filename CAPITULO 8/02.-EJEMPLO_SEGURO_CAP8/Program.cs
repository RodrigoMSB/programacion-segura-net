// ================================================================
//   Program.cs — Configuración Principal (Versión SEGURO)
// ================================================================
// Este archivo configura la aplicación ASP.NET Core para manejar
// excepciones de forma segura y profesional:
//
// ✔ Registra servicios de controladores y logging estructurado.
// ✔ Aplica un middleware global (UseExceptionHandler) para capturar
//   todas las excepciones no controladas en ambientes de producción.
// ✔ Muestra DeveloperExceptionPage solo en desarrollo para facilitar la depuración.
// ✔ Usa HSTS en producción para reforzar HTTPS.
// ================================================================

// ---------------------------------------------------------------
// Crear y configurar el builder de la aplicación.
// ---------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Registrar servicios de controladores y logging.
// ---------------------------------------------------------------
// Controllers: para soportar endpoints API REST.
// Logging: para registrar errores y eventos de forma estructurada.
builder.Services.AddControllers();
builder.Services.AddLogging();

// ---------------------------------------------------------------
// Construir la aplicación.
// ---------------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------------
// Configurar manejo de errores centralizado según el ambiente:
// ---------------------------------------------------------------
// - En PRODUCCIÓN: UseExceptionHandler redirige a /error.
// - También se habilita HSTS para reforzar el uso de HTTPS.
// - En DESARROLLO: se muestra DeveloperExceptionPage para depurar.
if (!app.Environment.IsDevelopment())
{
    // ✅ Configuración segura para producción.
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    // ✅ Solo en desarrollo, muestra detalles completos.
    app.UseDeveloperExceptionPage();
}

// ---------------------------------------------------------------
// Mapear rutas de controladores.
// ---------------------------------------------------------------

app.MapControllers();

// ---------------------------------------------------------------
// Iniciar la aplicación web.
// ---------------------------------------------------------------

app.Run();