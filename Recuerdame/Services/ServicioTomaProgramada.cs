using Recuerdame.Common;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Interfaces;

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
                Id = m.Id,
                MedicamentoId = m.MedicamentoId,
                FechaHoraProgramada = m.FechaHoraProgramada,
                FechaConfirmacion = m.FechaConfirmacion,
                EstadoToma = m.EstadoToma,
                MedicamentoNombre = m.Medicamento?.Nombre ?? string.Empty,
                CategoriaNombre = m.Medicamento?.CategoriaMedicamento?.Nombre ?? string.Empty,
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
