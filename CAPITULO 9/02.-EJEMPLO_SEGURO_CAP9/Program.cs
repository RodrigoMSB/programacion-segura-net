// ================================================================
//   Program.cs — Configuración Principal (Versión API REST FINAL)
// ================================================================
// ✔️ Configura el proyecto .NET como una API REST pura (sin vistas Razor).
// ✔️ Habilita Session con backend de cache en memoria (DistributedMemoryCache).
// ✔️ Registra CryptoService como singleton para hashing seguro.
// ✔️ Incluye middlewares clave: HTTPS, HSTS, Routing y Session.
// ================================================================

// ---------------------------------------------------------------
// 📦 IMPORTS — Espacios de nombres requeridos
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Builder;              // Para construir y configurar la app web.
using Microsoft.Extensions.DependencyInjection;   // Para DI: registrar servicios.
using Microsoft.Extensions.Hosting;               // Para manejar entorno dev/prod.
using EjemploSeguroCapitulo9.Services;            // Para usar CryptoService.

// ---------------------------------------------------------------
// 🚀 1️⃣ Crear el builder principal de la aplicación
// ---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// 🔑 2️⃣ Registrar servicios para la API REST segura
// ---------------------------------------------------------------

// ✅ Controladores RESTful
builder.Services.AddControllers();

// ✅ Backend de cache en memoria (requerido para usar Session)
builder.Services.AddDistributedMemoryCache();

// ✅ Habilitar Session: mantiene datos de autenticación temporal por usuario
builder.Services.AddSession();

// ✅ Registrar CryptoService como singleton: un solo hash engine para toda la app
builder.Services.AddSingleton<CryptoService>();

// ---------------------------------------------------------------
// 🏗️ 3️⃣ Construir la aplicación
// ---------------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------------
// 🧱 4️⃣ Configurar middlewares de seguridad y routing
// ---------------------------------------------------------------

// ✅ HSTS: obliga a HTTPS (en entornos producción)
app.UseHsts();

// ✅ Redireccionar peticiones HTTP a HTTPS automáticamente
app.UseHttpsRedirection();

// ✅ Enrutamiento para los controladores
app.UseRouting();

// ✅ Session debe ir después de Routing, antes de MapControllers
app.UseSession();

// ---------------------------------------------------------------
// 🗺️ 5️⃣ Mapear controladores API (detecta todos los [ApiController])
// ---------------------------------------------------------------
app.MapControllers();

// ---------------------------------------------------------------
// ▶️ 6️⃣ Ejecutar la aplicación
// ---------------------------------------------------------------
app.Run();