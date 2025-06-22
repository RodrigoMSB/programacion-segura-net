// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Este archivo configura la aplicación base para el
// Ejemplo Inseguro — Capítulo 5.
//
// Demuestra una configuración mínima y totalmente insegura:
// - No registra servicios de autenticación (JWT, cookies, OAuth2).
// - No establece políticas de autorización.
// - No aplica middlewares de validación de tokens.
// - Todos los endpoints están expuestos sin control.
//
// Sirve para contrastar directamente con la versión segura
// donde se implementa autenticación JWT y control de roles.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Proporciona el builder para construir y configurar la aplicación web.
using Microsoft.AspNetCore.Builder;

// Permite registrar servicios (como controladores) en el contenedor de dependencias.
using Microsoft.Extensions.DependencyInjection;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se crea una instancia del builder.
// Aquí se podrían registrar servicios de autenticación y autorización,
// pero intencionalmente se omiten.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE SERVICIOS (SIN SEGURIDAD)
// ---------------------------------------------------------------

// Solo se registran los controladores.
// No se agrega esquema de autenticación JWT, cookies seguras ni OAuth2.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye la aplicación usando los servicios definidos.
var app = builder.Build();

// ---------------------------------------------------------------
// MAPEADO DE CONTROLADORES
// ---------------------------------------------------------------

// Se mapean todos los controladores para exponer sus rutas.
// No se protege ninguna ruta; toda solicitud es aceptada.
app.MapControllers();

// ---------------------------------------------------------------
// EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se inicia la aplicación.
// No se ejecuta ningún middleware de validación de autenticación o autorización.
// Todos los endpoints quedan accesibles sin restricción.
app.Run();