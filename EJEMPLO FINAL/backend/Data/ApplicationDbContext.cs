using Microsoft.EntityFrameworkCore;
using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Data
{
    /// <summary>
    /// ApplicationDbContext es el punto de entrada para Entity Framework Core.
    /// Define las entidades y sus relaciones, y configura el acceso a SQLite.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // Constructor requerido por EF Core
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ================================
        // DbSets (Tablas)
        // ================================
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        // ================================
        // Configuración del Modelo
        // ================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================================
            // Tabla Usuario
            // ================================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique(); // Emails únicos
                entity.Property(u => u.Rol).HasDefaultValue("Cliente");
            });

            // ================================
            // Tabla CuentaBancaria
            // ================================
            modelBuilder.Entity<CuentaBancaria>(entity =>
            {
                entity.HasIndex(c => c.NumeroCuenta).IsUnique(); // Cuentas únicas

                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Cuentas)
                      .HasForeignKey(c => c.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ================================
            // Tabla Movimiento
            // ================================
            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasOne(m => m.CuentaOrigen)
                      .WithMany(c => c.MovimientosOrigen)
                      .HasForeignKey(m => m.CuentaOrigenId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.CuentaDestino)
                      .WithMany(c => c.MovimientosDestino)
                      .HasForeignKey(m => m.CuentaDestinoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(m => m.Monto)
                      .HasPrecision(18, 2);
            });
        }
    }
}