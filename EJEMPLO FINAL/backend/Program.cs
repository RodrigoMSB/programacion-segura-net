// ==========================================
// IMPORTACIONES NECESARIAS
// ==========================================

using Microsoft.AspNetCore.Authentication.JwtBearer;        // Middleware de autenticación basado en JWT
using Microsoft.EntityFrameworkCore;                        // ORM Entity Framework Core para acceso a datos
using Microsoft.IdentityModel.Tokens;                       // Clases para validación y firma de tokens JWT
using System.Text;                                          // Para codificar la clave secreta del token
using SeguridadBancoFinal.Data;                             // DbContext con entidades del dominio
using SeguridadBancoFinal.Services;                         // Servicios de negocio como AuthService y UsuarioService
using SeguridadBancoFinal.Middleware;                       // Middleware personalizado para manejo global de errores
using FluentValidation.AspNetCore;                          // Integración de validaciones con FluentValidation

// ==========================================
// CONFIGURACIÓN DE LA APLICACIÓN
// ==========================================

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURACIÓN DE LOGGING
// ==========================================
// Limpia cualquier proveedor previo y deja solo consola
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ==========================================
// CONFIGURACIÓN DE EF CORE CON SQLITE
// ==========================================
// Usa SQLite como base de datos relacional, ideal para entorno local/demo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// CONFIGURACIÓN DE AUTENTICACIÓN JWT
// ==========================================
// Se usa JWT para emitir y validar tokens de acceso
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Requiere token para cada request
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Parámetros de validación del token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // No validamos emisor
        ValidateAudience = false, // No validamos audiencia
        ValidateLifetime = true, // Token no debe estar expirado
        ValidateIssuerSigningKey = true, // Validar la firma del token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)) // Clave secreta compartida
    };
});

// ==========================================
// CONFIGURACIÓN DE AUTORIZACIÓN
// ==========================================
// Se activa el sistema de roles para uso de [Authorize(Roles = "...")]
builder.Services.AddAuthorization();

// ==========================================
// INYECCIÓN DE DEPENDENCIAS
// ==========================================
// Aquí se registran los servicios que serán inyectados vía constructor
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<AuthService>();           // Servicio JWT
builder.Services.AddScoped<CryptoService>();         // Hashing y cifrado

// ==========================================
// CONFIGURACIÓN DE CORS — PERMITIR FRONTEND LOCAL
// ==========================================
// Se permite explícitamente el origen del frontend para pruebas en local
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // URL del frontend en modo dev (Vue, React, etc.)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // Permitir cookies, auth headers, etc.
        });
});

// ==========================================
// CONFIGURACIÓN DE CONTROLLERS + FluentValidation
// ==========================================
// Integración del sistema de validación de DTOs
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Registra automáticamente todos los validadores de esta ensambladura
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = false; // Se mantiene compatibilidad con [DataAnnotations]
    });

// ==========================================
// CONSTRUCCIÓN FINAL DE LA APP
// ==========================================
var app = builder.Build();

// ==========================================
// LOG DE LA CADENA DE CONEXIÓN
// ==========================================
Console.WriteLine("USANDO CONNECTION STRING:");
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

// ==========================================
// CREACIÓN AUTOMÁTICA DE BASE DE DATOS SI NO EXISTE
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); // Crea la BD si no existe
}

// ==========================================
// MIDDLEWARE PERSONALIZADO PARA EXCEPCIONES
// ==========================================
// Captura todas las excepciones no manejadas en un solo punto
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ==========================================
// MIDDLEWARE DE REDIRECCIÓN A HTTPS
// ==========================================
app.UseHttpsRedirection(); // Fuerza redirección a HTTPS
app.UseCors(MyAllowSpecificOrigins); // Habilita política CORS definida

// ==========================================
// MIDDLEWARE DE SEGURIDAD
// ==========================================
app.UseAuthentication(); // JWT token bearer
app.UseAuthorization();  // Validación de roles

// ==========================================
// MAPEO DE CONTROLADORES
// ==========================================
app.MapControllers(); // Activa rutas desde [Route] en cada controller

// ==========================================
// INICIO DE LA APLICACIÓN
// ==========================================
app.Run(); // Lanza el servidor ASP.NET Core