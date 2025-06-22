// ================================================================
//   Program.cs — Configuración principal (Versión SEGURA)
// ================================================================
// Configura la aplicación aplicando validación de entradas segura,
// utilizando Data Annotations y FluentValidation de forma moderna,
// según prácticas recomendadas para FluentValidation v11+.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres correctos
// ---------------------------------------------------------------

using FluentValidation;                  // Para AddValidatorsFromAssemblyContaining
using FluentValidation.AspNetCore;       // Para AddFluentValidationAutoValidation
using Microsoft.AspNetCore.Builder;      // Para construir la app
using Microsoft.Extensions.DependencyInjection; // Para registrar servicios

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE CONTROLADORES Y FLUENTVALIDATION
// ---------------------------------------------------------------

// 1) Registrar controladores (MVC)
// 2) Registrar validación automática para ejecutar validadores al recibir solicitudes.
// 3) Registrar adaptadores para validación en cliente (opcional, útil para Razor Pages o Blazor).
// 4) Registrar todos los validadores encontrados en el ensamblado donde está Program.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// ---------------------------------------------------------------
// CONSTRUCCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------------
// MAPEADO DE CONTROLADORES
// ---------------------------------------------------------------

app.MapControllers();

// ---------------------------------------------------------------
// EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

app.Run();