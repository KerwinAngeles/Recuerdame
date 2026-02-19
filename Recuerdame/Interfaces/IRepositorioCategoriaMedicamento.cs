using Recuerdame.Common;
using Recuerdame.Dtos.CategoriaMedicamento;
using Recuerdame.Model;

namespace Recuerdame.Interfaces
{
    public interface IRepositorioCategoriaMedicamento : IRepositorioGenerico<CategoriaMedicamento>
    {
        Task<ResultadoPaginado<CategoriaMedicamento>> GetAllFiltrado(CategoriaMedicamentoFiltros filtros);
    }
}
