// ================================================================
//   Program.cs — Configuración principal (Versión SEGURO)
// ================================================================
// Este archivo configura la autenticación con JWT y el control
// de acceso basado en roles en ASP.NET Core.
//
// Forma parte del Ejemplo SEGURO — Capítulo 5, que demuestra
// cómo proteger endpoints críticos usando tokens firmados
// y políticas de autorización.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Importa tipos y servicios para habilitar autenticación
// mediante el esquema JWT Bearer.
using Microsoft.AspNetCore.Authentication.JwtBearer;

// Importa componentes de seguridad para validar y firmar el token.
using Microsoft.IdentityModel.Tokens;

// Proporciona utilidades para trabajar con codificación de texto.
using System.Text;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se crea una instancia del builder para configurar servicios
// y middlewares de la aplicación.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// DEFINICIÓN DE LA CLAVE SECRETA PARA JWT
// ---------------------------------------------------------------

// Esta clave se utiliza para firmar y validar el JWT.
// Importante: En producción se debe usar una variable de entorno
// o un gestor de secretos seguro, nunca hardcodear en el código.
var claveSecreta = "SuperClaveUltraMegaHiperSecreta123456789!"; // SOLO EJEMPLO

// ---------------------------------------------------------------
// CONFIGURACIÓN DE AUTENTICACIÓN CON JWT
// ---------------------------------------------------------------

// Registra el servicio de autenticación y especifica que se usará
// JWT Bearer como esquema predeterminado.
builder.Services.AddAuthentication(options =>
{
    // Esquema predeterminado para autenticar solicitudes entrantes.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Esquema predeterminado para desafiar solicitudes no autenticadas.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Parámetros de validación del token JWT.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // En este ejemplo se omite la validación de Emisor y Audiencia
        // para simplificar. En un entorno real se recomienda activarlas.
        ValidateIssuer = false,
        ValidateAudience = false,

        // Valida la vigencia temporal del token.
        ValidateLifetime = true,

        // Valida la firma del token.
        ValidateIssuerSigningKey = true,

        // Clave de firma usada para verificar el token recibido.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta))
    };
});

// ---------------------------------------------------------------
// REGISTRO DE SERVICIOS DE AUTORIZACIÓN Y CONTROLADORES
// ---------------------------------------------------------------

// Habilita servicios de autorización por roles y políticas.
builder.Services.AddAuthorization();

// Registra controladores para exponer los endpoints de la API.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Construye la aplicación con la configuración definida.
var app = builder.Build();

// ---------------------------------------------------------------
// USO DE MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN
// ---------------------------------------------------------------

// Middleware que valida automáticamente el JWT en cada solicitud.
app.UseAuthentication();

// Middleware que aplica políticas de autorización (roles, claims, etc.).
app.UseAuthorization();

// Mapea controladores para que respondan a las rutas definidas.
app.MapControllers();

// ---------------------------------------------------------------
// EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Inicia la aplicación y deja el servidor escuchando solicitudes.
app.Run();