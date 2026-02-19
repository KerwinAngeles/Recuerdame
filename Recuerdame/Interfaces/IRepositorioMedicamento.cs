using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Model;

namespace Recuerdame.Interfaces
{
    public interface IRepositorioMedicamento : IRepositorioGenerico<Medicamento>
    {
        Task InactivarMedicamento(int id);
        Task<ResultadoPaginado<Medicamento>> GetAllFiltrado(MedicamentoFiltros filtros);
    }
}
