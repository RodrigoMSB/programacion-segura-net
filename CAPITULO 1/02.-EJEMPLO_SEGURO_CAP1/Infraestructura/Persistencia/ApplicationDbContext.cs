// ================================================================
// ApplicationDbContext.cs — Contexto de datos de la aplicación
// ================================================================
// Define la unidad principal de trabajo para EF Core.
// Configura la conexión a la base de datos y expone las entidades 
// como propiedades DbSet para consulta y persistencia.
// ================================================================

// Importación de EF Core para soporte de DbContext y DbSet.
// Estas clases permiten mapear objetos .NET a registros de una base de datos relacional.
using Microsoft.EntityFrameworkCore;

// Importa el modelo de dominio Usuario que será gestionado por EF Core.
using EjemploSeguridadCapitulo1.Models;

namespace EjemploSeguridadCapitulo1.Data
{
    /// <summary>
    /// Representa el contexto principal de la aplicación para Entity Framework Core.
    /// Su función es administrar la conexión a la base de datos y el mapeo de entidades.
    /// 
    /// Diseño:
    /// - Simula una base de datos usando InMemoryDatabase para fines didácticos y pruebas.
    /// - Facilita el cambio a un motor real (SQL Server, PostgreSQL) sin alterar otras capas.
    /// - Aplica el principio de separación de responsabilidades dentro de una arquitectura limpia.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto. Recibe las opciones de configuración desde 
        /// el contenedor de dependencias para mayor flexibilidad.
        /// Permite cambiar el proveedor de base de datos sin modificar la clase.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Representa la tabla lógica de usuarios dentro del contexto de EF Core.
        /// EF Core usa esta propiedad para:
        /// - Generar la estructura de almacenamiento.
        /// - Hacer seguimiento de cambios sobre instancias de Usuario.
        /// - Ejecutar operaciones CRUD de forma segura.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Método opcional para personalizar cómo EF Core construye el modelo de datos.
        /// Se deja preparado para futuras extensiones, como aplicar configuraciones adicionales
        /// (restricciones, relaciones, seeds, etc.).
        /// </summary>
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     // Se pueden definir reglas adicionales aquí si se requiere.
        // }
    }
}