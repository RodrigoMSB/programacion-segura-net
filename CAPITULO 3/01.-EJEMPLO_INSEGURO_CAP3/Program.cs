// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Este archivo configura la aplicación para el ejemplo inseguro del
// Capítulo 3. Demuestra múltiples malas prácticas intencionales:
// - No se aplica validación de entrada ni middleware de seguridad.
// - No hay separación de responsabilidades mediante servicios o capas.
// - No se gestionan claves ni secretos de forma segura.
// - No se definen políticas de autenticación ni autorización.
// Este diseño viola principios clave como defensa en profundidad,
// mínimo privilegio y separación de responsabilidades.
// ================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye el builder sin registrar servicios de seguridad.
// No se usa autenticación, autorización ni validación de modelo.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE CONTROLADORES
// ---------------------------------------------------------------

// Se agregan los controladores de la API.
// No se implementan políticas de autorización ni middleware personalizado.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye la aplicación con configuración mínima.
var app = builder.Build();

// Se mapean las rutas de los controladores sin ningún filtro o validación.
app.MapControllers();

// Se ejecuta la aplicación sin HTTPS forzado ni configuración de entornos.
app.Run();