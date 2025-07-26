// ==============================================================================
// ApplicationDbContext.cs — Contexto Principal de EF Core para la Aplicación
// ==============================================================================
// Este archivo define el `ApplicationDbContext`, que actúa como la interfaz
// entre la aplicación y la base de datos relacional (SQLite en este caso),
// utilizando Entity Framework Core.
//
// Contiene:
// - Definición de las entidades (DbSet) que se mapearán como tablas.
// - Configuración de restricciones y relaciones entre entidades.
// - Reglas de comportamiento para claves foráneas, índices y eliminación.
//
// ------------------------------------------------------------------------------
// IMPORTS — Librerías necesarias para ORM y modelos de negocio
// ------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;              // Base de EF Core.
using SeguridadBancoFinal.Models;                // Modelos definidos por la aplicación.

namespace SeguridadBancoFinal.Data
{
    /// <summary>
    /// Contexto de base de datos para EF Core.
    /// Define el esquema relacional que será generado y accedido por la aplicación.
    /// Incluye mapeo de entidades, restricciones, y configuraciones adicionales.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // ==========================================================================
        // CONSTRUCTOR
        // ==========================================================================
        /// <summary>
        /// Constructor que permite inyectar opciones de configuración, como
        /// el proveedor de base de datos (SQLite, SQL Server, etc.).
        /// </summary>
        /// <param name="options">Opciones de configuración para DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ==========================================================================
        // DBSets — Definen las tablas de la base de datos
        // ==========================================================================
        /// <summary>
        /// Tabla de usuarios del sistema.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Tabla de cuentas bancarias asociadas a usuarios.
        /// </summary>
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }

        /// <summary>
        /// Tabla de movimientos (transferencias) entre cuentas.
        /// </summary>
        public DbSet<Movimiento> Movimientos { get; set; }

        /// <summary>
        /// Tabla de auditorías de transferencias, que registra email del usuario
        /// e IP de origen para propósitos de trazabilidad.
        /// </summary>
        public DbSet<AuditoriaTransferencia> AuditoriasTransferencias { get; set; }

        // ==========================================================================
        // ONMODEL CREATING — Personaliza el modelo al momento de generar la BD
        // ==========================================================================
        /// <summary>
        /// Configura relaciones, restricciones, índices e integridad referencial.
        /// Este método se ejecuta cuando EF construye el modelo a partir del código.
        /// </summary>
        /// <param name="modelBuilder">Builder que configura el modelo de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================================
            // CONFIGURACIÓN: Entidad Usuario
            // ==========================================================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique(); // Email debe ser único.
                entity.Property(u => u.Rol).HasDefaultValue("Cliente"); // Rol por defecto.
            });

            // ==========================================================
            // CONFIGURACIÓN: Entidad CuentaBancaria
            // ==========================================================
            modelBuilder.Entity<CuentaBancaria>(entity =>
            {
                entity.HasIndex(c => c.NumeroCuenta).IsUnique(); // Cada cuenta debe ser única.

                // Relación: Una cuenta pertenece a un único usuario.
                // Un usuario puede tener muchas cuentas.
                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Cuentas)
                      .HasForeignKey(c => c.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict); // Previene cascada.
            });

            // ==========================================================
            // CONFIGURACIÓN: Entidad Movimiento (transferencias)
            // ==========================================================
            modelBuilder.Entity<Movimiento>(entity =>
            {
                // Relación: Una transferencia tiene una cuenta de origen.
                // La cuenta origen puede aparecer en múltiples movimientos.
                entity.HasOne(m => m.CuentaOrigen)
                      .WithMany(c => c.MovimientosOrigen)
                      .HasForeignKey(m => m.CuentaOrigenId)
                      .OnDelete(DeleteBehavior.Restrict); // No eliminar en cascada

                // Relación: Una transferencia tiene una cuenta de destino.
                entity.HasOne(m => m.CuentaDestino)
                      .WithMany(c => c.MovimientosDestino)
                      .HasForeignKey(m => m.CuentaDestinoId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Formato decimal exacto para el campo Monto
                entity.Property(m => m.Monto)
                      .HasPrecision(18, 2); // Precisión SQL: DECIMAL(18,2)
            });
        }
    }
}