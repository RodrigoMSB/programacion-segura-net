// --------------------------------------------------------------------------------------------
// 📄 Program.cs — Versión INSEGURA, para fines educativos
// --------------------------------------------------------------------------------------------

// 📌 Importa los espacios de nombres básicos de ASP.NET Core para construir aplicaciones web.
using Microsoft.AspNetCore.Builder;      // Provee interfaces para construir la app HTTP.
using Microsoft.AspNetCore.Hosting;      // Maneja la configuración y el entorno de hosting.
using Microsoft.Extensions.Hosting;      // Permite determinar si el entorno es Desarrollo, Producción, etc.
using Microsoft.Extensions.DependencyInjection; // Permite registrar servicios como controladores.

// --------------------------------------------------------------------------------------------
// 🔑 Crear el constructor de la aplicación web.
// 'WebApplication.CreateBuilder' inicializa el host, carga configuración y prepara servicios.
var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------------------------------------------
// ❌ MALA PRÁCTICA:
// Se lee la cadena de conexión directamente desde 'appsettings.json',
// el cual incluye credenciales planas de base de datos (usuario y contraseña hardcodeados).
// Esto expone datos sensibles si el archivo se filtra o se sube a un repositorio público.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 📌 Se imprime la cadena de conexión en consola.
// ❌ MALA PRÁCTICA: Mostrar secretos en logs o consola facilita el robo de credenciales.
Console.WriteLine($"Usando conexión: {connectionString}");

// --------------------------------------------------------------------------------------------
// 📌 Registrar servicios de controladores en el contenedor de dependencias.
// Esto es necesario para usar controladores y rutas HTTP.
builder.Services.AddControllers();

// --------------------------------------------------------------------------------------------
// 🔑 Construir la aplicación web con la configuración y servicios definidos.
var app = builder.Build();

// --------------------------------------------------------------------------------------------
// ❌ MALA PRÁCTICA:
// No se agrega redirección forzada a HTTPS.
// La aplicación aceptará solicitudes HTTP sin cifrado, lo que permite ataques de sniffing o MITM.
app.MapControllers();

// --------------------------------------------------------------------------------------------
// 🔑 Ejecutar la aplicación.
// Inicia el listener HTTP en el puerto configurado y empieza a procesar solicitudes entrantes.
app.Run();