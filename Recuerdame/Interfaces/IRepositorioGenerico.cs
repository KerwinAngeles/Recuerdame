namespace Recuerdame.Interfaces
{
    public interface IRepositorioGenerico<Entity> where Entity : class
    {
        Task AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(Entity entity);
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetAll();
    }
}
