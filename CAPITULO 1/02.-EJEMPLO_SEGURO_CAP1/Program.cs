// ================================================================
// Program.cs — Punto de entrada principal de la aplicación web
// ================================================================
// Define la configuración de servicios y el pipeline de ejecución
// de la aplicación ASP.NET Core.
// Incluye configuraciones de seguridad, Swagger y base de datos InMemory.
// ================================================================

// ----------------------------------------------------------------
// IMPORTS
// ----------------------------------------------------------------

// Permite construir y configurar la aplicación web.
using Microsoft.AspNetCore.Builder;

// Permite acceder a información del entorno (Development, Production).
using Microsoft.AspNetCore.Hosting;

// Permite registrar servicios y configurarlos para inyección de dependencias.
using Microsoft.Extensions.DependencyInjection;

// Permite configurar el host de la aplicación y gestionar su ciclo de vida.
using Microsoft.Extensions.Hosting;

// Permite trabajar con Entity Framework Core usando base de datos en memoria.
using Microsoft.EntityFrameworkCore;

// Importa el contexto de base de datos definido en la aplicación.
using EjemploSeguridadCapitulo1.Data;

// Importa los servicios de negocio definidos en la aplicación.
using EjemploSeguridadCapitulo1.Services;

// ----------------------------------------------------------------
// CREACIÓN Y CONFIGURACIÓN DE LA APLICACIÓN
// ----------------------------------------------------------------

// Crea el builder de la aplicación web (patrón builder).
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE SERVICIOS PARA INYECCIÓN DE DEPENDENCIAS
// ---------------------------------------------------------------

// Registra controladores (API) con soporte para atributos como [HttpGet].
builder.Services.AddControllers();

// Registra servicios necesarios para Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura Entity Framework Core para usar una base de datos en memoria.
// Esta base se reinicia cada vez que se reinicia la aplicación.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BD_Seguridad"));

// Registra el servicio de usuarios y el servicio de autenticación
// para que se inyecten automáticamente en los controladores.
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<AutenticacionService>();

// ----------------------------------------------------------------
// CONSTRUCCIÓN DE LA APLICACIÓN
// ----------------------------------------------------------------

var app = builder.Build();

// ----------------------------------------------------------------
// CONFIGURACIÓN DEL PIPELINE HTTP
// ----------------------------------------------------------------

// Habilita Swagger y su UI solo en entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Define puertos de escucha explícitamente.
app.Urls.Add("https://localhost:5001");
app.Urls.Add("http://localhost:5000");

// Redirige solicitudes HTTP a HTTPS automáticamente.
app.UseHttpsRedirection();

// Habilita la autorización (en este ejemplo no hay autenticación completa aún).
app.UseAuthorization();

// Mapea los controladores a las rutas HTTP correspondientes.
app.MapControllers();

// Inicia la ejecución de la aplicación.
app.Run();