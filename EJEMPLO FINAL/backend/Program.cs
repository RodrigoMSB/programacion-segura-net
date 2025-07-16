using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SeguridadBancoFinal.Data;
using SeguridadBancoFinal.Services;
using SeguridadBancoFinal.Middleware;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONFIGURACIÓN DE LOGGING
// ==========================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ==========================================
// CONFIGURACIÓN DE EF CORE CON SQLITE
// ==========================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// CONFIGURACIÓN DE AUTENTICACIÓN JWT
// ==========================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };
});

// ==========================================
// CONFIGURACIÓN DE AUTORIZACIÓN
// ==========================================
builder.Services.AddAuthorization();

// ==========================================
// INYECCIÓN DE DEPENDENCIAS
// ==========================================
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CryptoService>();

// ==========================================
// CONFIGURACIÓN DE CORS — PERMITIR FRONTEND
// ==========================================
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // Tu frontend
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// ==========================================
// CONFIGURACIÓN DE CONTROLLERS + FluentValidation
// ==========================================
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = false;
    });

builder.Services.AddScoped<ICuentaService, CuentaService>();

// ==========================================
// CONSTRUCCIÓN DE LA APP
// ==========================================
var app = builder.Build();

Console.WriteLine("USANDO CONNECTION STRING:");
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// ==========================================
// MIDDLEWARE PERSONALIZADO
// ==========================================
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ==========================================
// MIDDLEWARE DE HTTPS
// ==========================================
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

// ==========================================
// MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN
// ==========================================
app.UseAuthentication();
app.UseAuthorization();

// ==========================================
// MAPEO DE ENDPOINTS
// ==========================================
app.MapControllers();

// ==========================================
// EJECUCIÓN DE LA APP
// ==========================================
app.Run();