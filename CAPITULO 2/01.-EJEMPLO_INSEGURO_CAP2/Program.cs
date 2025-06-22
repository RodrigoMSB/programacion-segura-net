// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Este archivo configura la aplicación web y define el contexto de
// base de datos para la API de reportes médicos. Esta versión no
// aplica medidas de seguridad ni separación de capas, ilustrando un
// proyecto sin levantamiento de requisitos de seguridad adecuado.
// ================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EjemploInseguroCapitulo2.Models;

// ---------------------------------------------------------------
// CREACIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se crea el builder para registrar servicios y middlewares.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DEL CONTEXTO DE BASE DE DATOS (INMEMORY)
// ---------------------------------------------------------------

// Se registra el contexto de base de datos en memoria.
// No se usa una cadena de conexión externa ni configuración en appsettings.
builder.Services.AddDbContext<ContextoBaseDatos>(options =>
    options.UseInMemoryDatabase("BaseDatosReportes"));

// ---------------------------------------------------------------
// REGISTRO DE CONTROLADORES
// ---------------------------------------------------------------

// Se agrega soporte para controladores.
// No se añade autenticación ni autorización.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Construye la aplicación con los servicios configurados.
var app = builder.Build();

// Mapea las rutas de los controladores.
app.MapControllers();

// Ejecuta la aplicación.
app.Run();

// ================================================================
//   ContextoBaseDatos — DbContext (Versión INSEGURA)
// ================================================================
// Define la clase ContextoBaseDatos que representa la unidad de trabajo
// para la base de datos en memoria. Se accede directamente desde el
// controlador sin capa de servicio ni repositorio.
// ================================================================

/// <summary>
/// Contexto de base de datos para la API de reportes médicos.
/// Contiene un DbSet para manejar los objetos ReporteMedico.
/// </summary>
public class ContextoBaseDatos : DbContext
{
    /// <summary>
    /// Constructor que recibe opciones de configuración.
    /// </summary>
    public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options) { }

    /// <summary>
    /// Conjunto de reportes médicos.
    /// Se expone directamente sin encapsulamiento.
    /// </summary>
    public DbSet<ReporteMedico> ReportesMedicos => Set<ReporteMedico>();
}