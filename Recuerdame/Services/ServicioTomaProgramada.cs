using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Interfaces;
using Recuerdame.Model;

namespace Recuerdame.Services
{
    public class ServicioTomaProgramada
    {
        private readonly IRepositorioTomaProgramada _repositorioTomaProgramda;
        public ServicioTomaProgramada(IRepositorioTomaProgramada repositorioTomaProgramada)
        {
            _repositorioTomaProgramda = repositorioTomaProgramada; 
        }

        public async Task<ResultadoPaginado<TomaPogramadaDto>> AllTomasProgramadas(TomaProgramadaFiltro filtros)
        {
            var result = await _repositorioTomaProgramda.GetAllFiltrado(filtros);
            var items = result.Items.Select(m => new TomaPogramadaDto
            {
                MedicamentoId = m.MedicamentoId,
                FechaHoraProgramada = m.FechaHoraProgramada,
                FechaConfirmacion = m.FechaConfirmacion,
                EstadoToma = m.EstadoToma,
                MedicamentoNombre = m.Medicamento.Nombre,
                CategoriaNombre = m.Medicamento.CategoriaMedicamento.Nombre

            }).ToList();

            return new ResultadoPaginado<TomaPogramadaDto>
            {
                Items = items,
                TotalRegistros = result.TotalRegistros,
                PaginaActual = result.PaginaActual,
                TamanoPagina = result.TamanoPagina,
            };
        }
    }
}
