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
                Id = m.Id,
                MedicamentoId = m.MedicamentoId,
                FechaHoraProgramada = m.FechaHoraProgramada,
                FechaConfirmacion = m.FechaConfirmacion,
                EstadoToma = m.EstadoToma,
                MedicamentoNombre = m.Medicamento.Nombre,
                CategoriaNombre = m.Medicamento.CategoriaMedicamento.Nombre,
                Dosis = m.Medicamento.Dosis,
                FrecuenciaHora = m.Medicamento.FrecuenciaHora,

            }).ToList();

            return new ResultadoPaginado<TomaPogramadaDto>
            {
                Items = items,
                TotalRegistros = result.TotalRegistros,
                PaginaActual = result.PaginaActual,
                TamanoPagina = result.TamanoPagina,
            };
        }

        public async Task<TomaPogramadaDto> UpdateTomaProgramada(int id, TomaProgramadaRequest request)
        {
            var tomaProgramada = await _repositorioTomaProgramda.GetById(id);

            tomaProgramada.FechaConfirmacion = request.FechaConfirmacion;
            tomaProgramada.EstadoToma = request.EstadoToma;
            await _repositorioTomaProgramda.UpdateAsync(tomaProgramada, id);

            return new TomaPogramadaDto
            {
                Id = tomaProgramada.Id,
                MedicamentoId = tomaProgramada.MedicamentoId,
                FechaHoraProgramada = tomaProgramada.FechaHoraProgramada,
                FechaConfirmacion = tomaProgramada.FechaConfirmacion,
                EstadoToma = tomaProgramada.EstadoToma,
            };
        }
    }
}
