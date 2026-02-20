using Recuerdame.Common;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Model;

namespace Recuerdame.Interfaces
{
    public interface IRepositorioTomaProgramada : IRepositorioGenerico<TomaProgramada>
    {
        Task<ResultadoPaginado<TomaProgramada>> GetAllFiltrado(TomaProgramadaFiltro filtros);
    }
}
