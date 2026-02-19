using Recuerdame.Interfaces;
using Recuerdame.Model;
using Recuerdame.Persistence;

namespace Recuerdame.Repositories
{
    public class RepositorioTomaProgramada : RepositorioGenerico<TomaProgramada>, IRepositorioTomaProgramada
    {
        private readonly ApplicationDbContext _context;
        public RepositorioTomaProgramada(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

    }
}
