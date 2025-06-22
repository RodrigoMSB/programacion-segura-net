// --------------------------------------------------------------------------------------------
// 📄 Program.cs — Versión SEGURA, aplicando buenas prácticas de configuración y ejecución.
// --------------------------------------------------------------------------------------------

// --------------------------------------------------------------------------------------------
// 📌 Importar los espacios de nombres principales de ASP.NET Core.
// - Microsoft.AspNetCore.Builder: Contiene la funcionalidad principal para construir la app web.
// - Microsoft.Extensions.Hosting: Permite identificar y configurar el entorno (Desarrollo, Producción, etc.).
// - Microsoft.Extensions.DependencyInjection: Proporciona el contenedor de dependencias para registrar servicios.
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// --------------------------------------------------------------------------------------------
// 🔑 Crear el constructor de la aplicación web.
// 'WebApplication.CreateBuilder' configura el host, carga la configuración y registra los servicios necesarios.
var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------------------------------------------
// ✅ BUENA PRÁCTICA:
// Obtener la cadena de conexión desde la configuración segura (por ejemplo, variables de entorno, secretos en Key Vault).
// Nunca se incluye directamente en el archivo appsettings.json.
// Si no se encuentra, se muestra un mensaje genérico y NO la cadena real.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Connection string not set.";

// 📌 Confirmar por consola que se obtuvo la configuración de forma segura.
// A diferencia del ejemplo inseguro, aquí NO se imprime la cadena real para evitar exposición de datos sensibles.
Console.WriteLine("Conexión obtenida de configuración segura.");

// --------------------------------------------------------------------------------------------
// 📌 Registrar el servicio de controladores en el contenedor de inyección de dependencias.
// Permite usar rutas y controladores para manejar peticiones HTTP.
builder.Services.AddControllers();

// --------------------------------------------------------------------------------------------
// 🔑 Construir la aplicación con la configuración y los servicios definidos.
var app = builder.Build();

// --------------------------------------------------------------------------------------------
// ✅ BUENA PRÁCTICA:
// Habilitar redirección automática de HTTP a HTTPS.
// Esto asegura que todas las solicitudes se cifren, protegiendo datos en tránsito contra sniffing o MITM.
app.UseHttpsRedirection();

// 📌 Mapear rutas de controladores.
// Aquí se activan los endpoints HTTP definidos en los controladores de la aplicación.
app.MapControllers();

// --------------------------------------------------------------------------------------------
// 🔑 Ejecutar la aplicación.
// Inicia el servidor web y queda a la espera de peticiones en el puerto configurado.
app.Run();