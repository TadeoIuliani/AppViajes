using AppViajesWirsolut.Models;
using Microsoft.EntityFrameworkCore;

namespace AppViajesWirsolut.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Viaje> Viajes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la relación entre Ciudad y Viaje (1:M)
            modelBuilder.Entity<Ciudad>()
                .HasMany(c => c.Viajes)
                .WithOne(v => v.Ciudad)
                .HasForeignKey(v => v.CiudadId);

            // Configuración para la relación entre Vehiculo y Viaje (M:M)
            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Viajes)
                .WithOne(v => v.Vehiculo)
                .HasForeignKey(v => v.VehiculoId);

            modelBuilder.Entity<Vehiculo>()
                        .HasIndex(v => v.Patente)
                        .IsUnique();

            modelBuilder.Entity<Ciudad>()
                .HasIndex(c => c.IdApiCiudad)
                .IsUnique();
        }
    }
}
