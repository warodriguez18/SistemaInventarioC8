using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioC8.Modelos;
using System.Reflection;

namespace SistemaInventarioC8.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            var entidadesModificadas = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entidad in entidadesModificadas)
            {
                if (entidad.Entity.GetType().GetProperty("FechaModificacion") != null)
                {
                    entidad.Property("FechaModificacion").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entidadesModificadas = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entidad in entidadesModificadas)
            {
                if (entidad.Entity.GetType().GetProperty("FechaModificacion") != null)
                {
                    entidad.Property("FechaModificacion").CurrentValue = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
