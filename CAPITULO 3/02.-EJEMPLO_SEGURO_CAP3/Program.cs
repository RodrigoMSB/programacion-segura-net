// ================================================================
//   Program.cs — Configuración principal (Versión SEGURA)
// ================================================================
// Este archivo configura la aplicación para el ejemplo seguro del
// Capítulo 3. Aplica múltiples principios de diseño seguro:
// - Separación de responsabilidades mediante servicios dedicados.
// - Validación de entrada con atributos de modelo y ModelState.
// - Roles y claims simulados a través de headers.
// - Gestión de secretos mediante variables de entorno simuladas.
// - Registro de logs para auditoría básica.
// ================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EjemploSeguroCapitulo3.Services;
using EjemploSeguroCapitulo3.Models;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye el builder principal que registra servicios,
// middlewares y configuraciones necesarias.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DE SERVICIOS DE APLICACIÓN
// ---------------------------------------------------------------

// Se registra el servicio de usuarios como singleton.
// Esto separa la lógica de negocio del controlador,
// aplicando el principio de separación de responsabilidades.
builder.Services.AddSingleton<UsuarioService>();

// ---------------------------------------------------------------
// CONFIGURACIÓN DE CONTROLADORES
// ---------------------------------------------------------------

// Se agregan los controladores de la API.
// ASP.NET Core gestiona la validación de modelos automáticamente
// gracias a los atributos de datos en la clase Usuario.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONFIGURACIÓN DE LOGGING
// ---------------------------------------------------------------

// Configura el logging básico para registrar eventos,
// lo que facilita la auditoría y el monitoreo de actividad.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye la aplicación con todos los servicios configurados.
var app = builder.Build();

// Redirige todo el tráfico HTTP a HTTPS para proteger la comunicación.
app.UseHttpsRedirection();

// Aplica autorización (en este ejemplo, se simula mediante headers en los controladores).
app.UseAuthorization();

// Mapea los controladores para habilitar las rutas de la API.
app.MapControllers();

// Ejecuta la aplicación.
app.Run();