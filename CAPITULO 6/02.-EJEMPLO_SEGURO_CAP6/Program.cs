// ================================================================
//   Program.cs — Configuración Principal (Versión SEGURO)
// ================================================================
// Configura la aplicación ASP.NET Core para:
// - Inyectar el servicio de criptografía (CryptoService).
// - Habilitar controladores REST.
// - Mapear rutas y ejecutar el servidor.
//
// Este archivo sigue el estilo minimal API de .NET 8.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Registra dependencias propias del proyecto.
// ---------------------------------------------------------------

// Importa la definición de CryptoService para registrar su inyección de dependencias.
using EjemploSeguroCapitulo6.Services;

// ---------------------------------------------------------------
// CONFIGURACIÓN DEL BUILDER Y REGISTRO DE SERVICIOS.
// ---------------------------------------------------------------

// Crea el builder principal de la aplicación web.
var builder = WebApplication.CreateBuilder(args);

// Registra el servicio de criptografía como Singleton:
// - Singleton: se instancia una sola vez y se reutiliza en toda la vida de la app.
builder.Services.AddSingleton<CryptoService>();

// Agrega soporte para controladores API REST.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y CONFIGURACIÓN DEL PIPELINE.
// ---------------------------------------------------------------

// Construye la aplicación.
var app = builder.Build();

// Habilita el enrutamiento y mapeo de controladores.
app.MapControllers();

// Ejecuta la aplicación y la deja escuchando solicitudes HTTP.
app.Run();