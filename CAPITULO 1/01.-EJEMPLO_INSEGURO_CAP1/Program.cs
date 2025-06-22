// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Este archivo define la configuración de arranque de la aplicación.
// Contiene el registro del contexto de base de datos en memoria
// y la inicialización de los controladores de forma simplificada.
// No implementa seguridad ni separación de capas adecuada.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EjemploInseguroCapitulo1.Models;

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y CONFIGURACIÓN DEL SERVIDOR
// ---------------------------------------------------------------

// Se crea un objeto builder para configurar servicios y middlewares.
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// REGISTRO DEL CONTEXTO DE BASE DE DATOS
// ---------------------------------------------------------------

// Agregamos el contexto de base de datos en memoria al contenedor de servicios.
// Se usa InMemoryDatabase sin cadena de conexión ni configuración externa.
// En este ejemplo INSEGURO se usa todo hardcodeado.
builder.Services.AddDbContext<ContextoBaseDatos>(options =>
    options.UseInMemoryDatabase("BaseDatosInsegura"));

// ---------------------------------------------------------------
// REGISTRO DE CONTROLADORES
// ---------------------------------------------------------------

// Registramos el soporte para controladores.
// No se añaden middlewares de autenticación ni filtros de autorización.
builder.Services.AddControllers();

// ---------------------------------------------------------------
// CONSTRUCCIÓN Y EJECUCIÓN DE LA APLICACIÓN
// ---------------------------------------------------------------

// Construimos la aplicación con los servicios configurados.
var app = builder.Build();

// Mapeamos las rutas de los controladores.
// No hay protección de rutas ni políticas de seguridad.
app.MapControllers();

// Ejecutamos la aplicación.
app.Run();

// ================================================================
//   ContextoBaseDatos — DbContext (Versión INSEGURA)
// ================================================================
// Define el contexto de base de datos para EF Core InMemory.
// No implementa separación por capas ni patrón repositorio.
// ================================================================

/// <summary>
/// Contexto de base de datos de la aplicación.
/// Usado directamente desde los controladores,
/// lo cual es una mala práctica en proyectos reales.
/// </summary>
public class ContextoBaseDatos : DbContext
{
    /// <summary>
    /// Constructor que recibe opciones de configuración.
    /// </summary>
    public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options) { }

    /// <summary>
    /// Conjunto de Usuarios.
    /// Se manipula directamente en controladores, sin capa de servicio.
    /// </summary>
    public DbSet<Usuario> Usuarios => Set<Usuario>();
}