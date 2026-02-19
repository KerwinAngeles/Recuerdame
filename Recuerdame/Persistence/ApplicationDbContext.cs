using Microsoft.EntityFrameworkCore;
using Recuerdame.Model;

namespace Recuerdame.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<CategoriaMedicamento> CategoriaMedicamentos { get; set; }
        public DbSet<TomaProgramada> TomaProgramada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medicamento>()
                .HasMany(m => m.tomaProgramadas)
                .WithOne()
                .HasForeignKey(m => m.MedicamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoriaMedicamento>()
                .HasMany(ct => ct.Medicamentos)
                .WithOne()
                .HasForeignKey(ct => ct.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
