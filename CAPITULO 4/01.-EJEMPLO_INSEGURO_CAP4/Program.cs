// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Este archivo configura la aplicación para el ejemplo inseguro
// del Capítulo 4. Muestra una configuración mínima, sin:
//
// - Validación de modelos con Data Annotations o FluentValidation.
// - Middleware de sanitización de entradas.
// - Manejo de errores centralizado.
// - Políticas de seguridad básica.
//
// Sirve para ilustrar cómo la falta de controles de entrada
// deja a la aplicación vulnerable a inyecciones y entradas maliciosas.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Dependencias principales de ASP.NET Core
// ---------------------------------------------------------------

// Importa el espacio de nombres para configurar la aplicación web.
using Microsoft.AspNetCore.Builder;

// Importa el espacio de nombres para registrar y resolver servicios,
// como controladores y validadores.
using Microsoft.Extensions.DependencyInjection;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se instancia el builder que permite registrar servicios
// y configurar el pipeline de la aplicación.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE SERVICIOS (SIN VALIDACIÓN)
// ---------------------------------------------------------------

// Registra los controladores de la API.
// En esta versión insegura, no se configuran validadores externos,
// ni se aplican filtros de sanitización o políticas de seguridad.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Construye la aplicación con los servicios y configuraciones
// definidos en el builder.
var app = builder.Build();

// ---------------------------------------------------------------
// MAPEADO DE RUTAS
// ---------------------------------------------------------------

// Mapea los controladores para exponer los endpoints.
// No hay middleware de validación, autorización ni manejo de excepciones global.
app.MapControllers();

// ---------------------------------------------------------------
// EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Ejecuta la aplicación. Todos los endpoints estarán accesibles
// sin validación ni controles de seguridad de entrada.
app.Run();