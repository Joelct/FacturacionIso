using FacturacionIso.Models;
using Microsoft.EntityFrameworkCore;

namespace FacturacionIso.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<AsientosContables> AsientosContables { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Facturacion> Facturacion { get; set; }
        public DbSet<Vendedores> Vendedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir claves primarias
            modelBuilder.Entity<Articulos>()
                .HasKey(a => a.IdArticulo);

            modelBuilder.Entity<AsientosContables>()
                .HasKey(ac => ac.IdAsiento);

            modelBuilder.Entity<Clientes>()
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<Facturacion>()
                .HasKey(f => f.IdFacturacion);

            modelBuilder.Entity<Vendedores>()
                .HasKey(v => v.IdVendedor);
        }
    }
}
