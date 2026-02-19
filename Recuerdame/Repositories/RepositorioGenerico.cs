using Microsoft.EntityFrameworkCore;
using Recuerdame.Interfaces;
using Recuerdame.Persistence;

namespace Recuerdame.Repositories
{
    public class RepositorioGenerico<Entity> : IRepositorioGenerico<Entity> where Entity : class
    {
        private readonly ApplicationDbContext _context;
        public RepositorioGenerico(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            Entity entry = await _context.Set<Entity>().FindAsync(id);
            if (entry == null) throw new Exception("Entidad no encontrada.");

            var entryEntry = _context.Entry(entry);
            var newValues = _context.Entry(entity).CurrentValues;

            foreach (var property in newValues.Properties)
            {
                // Ignorar la clave primaria
                if (property.IsPrimaryKey())
                    continue;

                var value = newValues[property];
                entryEntry.Property(property.Name).CurrentValue = value;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar los cambios: " + ex.Message);

                if (ex.InnerException != null)
                    Console.WriteLine("Detalle: " + ex.InnerException.Message);

                throw;
            }
        }

        public async Task DeleteAsync(Entity entity) 
        { 
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<Entity> GetById(int id) 
        { 

            return await _context.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<List<Entity>> GetAll()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

    }
}
