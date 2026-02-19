using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Excepciones;
using Recuerdame.Interfaces;
using Recuerdame.Model;

namespace Recuerdame.Services
{
    public class ServicioMedicamento
    {
        private readonly IRepositorioMedicamento _repositorioMedicamento;
        private readonly IRepositorioTomaProgramada _repositorioTomaProgramada;

        public ServicioMedicamento(
            IRepositorioMedicamento repositorioMedicamento,
            IRepositorioTomaProgramada repositorioTomaProgramada)
        {
            _repositorioMedicamento = repositorioMedicamento;
            _repositorioTomaProgramada = repositorioTomaProgramada;
        }

        public async Task<ResultadoPaginado<MedicamentoDto>> AllMedicamentos(MedicamentoFiltros filtros)
        {
            var resultado = await _repositorioMedicamento.GetAllFiltrado(filtros);

            var items = resultado.Items.Select(m => new MedicamentoDto
            {
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                Dosis = m.Dosis,
                FrecuenciaHora = m.FrecuenciaHora,
                FechaInicio = m.FechaInicio,
                FechaFinal = m.FechaFinal,
                Nota = m.Nota,
                CategoriasName = m.CategoriaMedicamento is not null
                    ? [m.CategoriaMedicamento.Nombre]
                    : []
            }).ToList();

            return new ResultadoPaginado<MedicamentoDto>
            {
                Items = items,
                TotalRegistros = resultado.TotalRegistros,
                PaginaActual = resultado.PaginaActual,
                TamanoPagina = resultado.TamanoPagina
            };
        }

        public async Task<MedicamentoDto> AddMedicamento(MedicamentoRequest request)
        {
            if (request.FechaFinal <= request.FechaInicio)
                throw new BusinessException("La fecha final debe ser posterior a la fecha de inicio.");

            if (request.FrecuenciaHora <= 0)
                throw new BusinessException("La frecuencia en horas debe ser mayor a cero.");

            var medicamento = new Medicamento
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Dosis = request.Dosis,
                FrecuenciaHora = request.FrecuenciaHora,
                FechaInicio = request.FechaInicio,
                FechaFinal = request.FechaFinal,
                Nota = request.Nota,
                EstaActivo = true
            };

            await _repositorioMedicamento.AddAsync(medicamento);

            var fechaActual = medicamento.FechaInicio;
            while (fechaActual <= medicamento.FechaFinal)
            {
                await _repositorioTomaProgramada.AddAsync(new TomaProgramada
                {
                    MedicamentoId = medicamento.Id,
                    FechaHoraProgramada = fechaActual,
                    EstadoToma = Enums.EstadoToma.Pendiente
                });
                fechaActual = fechaActual.AddHours(medicamento.FrecuenciaHora);
            }

            return new MedicamentoDto
            {
                Nombre = medicamento.Nombre,
                Descripcion = medicamento.Descripcion,
                Dosis = medicamento.Dosis,
                FrecuenciaHora = medicamento.FrecuenciaHora,
                FechaInicio = medicamento.FechaInicio,
                FechaFinal = medicamento.FechaFinal,
                Nota = medicamento.Nota
            };
        }

        public async Task<MedicamentoDto> UpdateMedicamento(int id, MedicamentoRequest request)
        {
            var medicamento = await _repositorioMedicamento.GetById(id)
                ?? throw new NotFoundException("Medicamento", id);

            medicamento.Nombre = request.Nombre;
            medicamento.Descripcion = request.Descripcion;
            medicamento.Dosis = request.Dosis;
            medicamento.FrecuenciaHora = request.FrecuenciaHora;
            medicamento.FechaInicio = request.FechaInicio;
            medicamento.FechaFinal = request.FechaFinal;
            medicamento.Nota = request.Nota;

            await _repositorioMedicamento.UpdateAsync(medicamento, id);

            return new MedicamentoDto
            {
                Nombre = medicamento.Nombre,
                Descripcion = medicamento.Descripcion,
                Dosis = medicamento.Dosis,
                FrecuenciaHora = medicamento.FrecuenciaHora,
                FechaInicio = medicamento.FechaInicio,
                FechaFinal = medicamento.FechaFinal,
                Nota = medicamento.Nota
            };
        }

        public async Task InactivarMedicamento(int id)
        {
            _ = await _repositorioMedicamento.GetById(id)
                ?? throw new NotFoundException("Medicamento", id);

            await _repositorioMedicamento.InactivarMedicamento(id);
        }
    }
}
