using Microsoft.EntityFrameworkCore;
using WebListaMercado.Data.Map;
using WebListaMercado.Models;

namespace WebListaMercado.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ComprasModel> Compras { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComprasMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
