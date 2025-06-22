// ================================================================
//   Program.cs — Configuración principal (Versión SEGURA)
// ================================================================
// Este archivo define la configuración principal de la aplicación
// para la API de reportes médicos. En esta versión se implementa
// un levantamiento de requisitos de seguridad correcto:
// - Identificación y protección de activos sensibles.
// - Separación de lógica de negocio.
// - Control de roles para restringir acceso.
// - Validación de datos desde el diseño.
// ================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EjemploSeguroCapitulo2.Models;
using EjemploSeguroCapitulo2.Services;

// ---------------------------------------------------------------
// CONSTRUCCIÓN DEL BUILDER DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se crea el builder para registrar todos los servicios de la aplicación.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DEL CONTEXTO DE BASE DE DATOS (INMEMORY)
// ---------------------------------------------------------------

// Se registra el contexto de base de datos en memoria.
// Nombre explícito para diferenciarlo de la versión insegura.
builder.Services.AddDbContext<ContextoBaseDatos>(options =>
    options.UseInMemoryDatabase("BaseDatosReportesSegura"));

// ---------------------------------------------------------------
// REGISTRO DE LA CAPA DE SERVICIO
// ---------------------------------------------------------------

// Se registra la capa de servicio ReporteMedicoService.
// Este servicio encapsula la lógica de negocio para cumplir
// con la separación de responsabilidades.
builder.Services.AddScoped<ReporteMedicoService>();

// ---------------------------------------------------------------
// REGISTRO DE CONTROLADORES
// ---------------------------------------------------------------

// Se agrega soporte para controladores de API.
// Permite el enrutamiento automático de las rutas definidas.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Se construye la aplicación con los servicios configurados.
var app = builder.Build();

// Se mapean las rutas de todos los controladores.
app.MapControllers();

// Se inicia la aplicación y queda escuchando en el puerto definido.
app.Run();

// ================================================================
//   ContextoBaseDatos — DbContext (Versión SEGURA)
// ================================================================
// Define la clase ContextoBaseDatos que representa la unidad de trabajo
// para la base de datos en memoria. Contiene un DbSet específico para
// reportes médicos y se inyecta en la capa de servicio.
// ================================================================

/// <summary>
/// Contexto de base de datos para la API de reportes médicos.
/// Gestiona la persistencia de los datos en memoria.
/// </summary>
public class ContextoBaseDatos : DbContext
{
    /// <summary>
    /// Constructor que recibe las opciones de configuración.
    /// </summary>
    public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options) { }

    /// <summary>
    /// Conjunto de reportes médicos.
    /// Protegido mediante validación y control de acceso desde la lógica de negocio.
    /// </summary>
    public DbSet<ReporteMedico> ReportesMedicos => Set<ReporteMedico>();
}