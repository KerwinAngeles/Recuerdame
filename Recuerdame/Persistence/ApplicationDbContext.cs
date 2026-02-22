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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Auditoria>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FechaCreacion = DateTime.Now;
                        entry.Entity.CreadoPor = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.FechaModificacion = DateTime.Now;
                        entry.Entity.ModificadoPor = "DefaultAppUser";
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medicamento>()
                .HasOne(m => m.CategoriaMedicamento)
                .WithMany(c => c.Medicamentos)
                .HasForeignKey(m => m.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TomaProgramada>()
                .HasOne(tp => tp.Medicamento)
                .WithMany(m => m.tomaProgramadas)
                .HasForeignKey(tp => tp.MedicamentoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
