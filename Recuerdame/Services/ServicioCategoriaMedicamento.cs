using Recuerdame.Common;
using Recuerdame.Dtos.CategoriaMedicamento;
using Recuerdame.Excepciones;
using Recuerdame.Interfaces;
using Recuerdame.Model;

namespace Recuerdame.Services
{
    public class ServicioCategoriaMedicamento
    {
        private readonly IRepositorioCategoriaMedicamento _repositorioCategoriaMedicamento;
        public ServicioCategoriaMedicamento(IRepositorioCategoriaMedicamento categoriaMedicamento)
        {
            _repositorioCategoriaMedicamento = categoriaMedicamento;
        }

        public async Task<CategoriaMedicamentoResponse> AddCategoriaMedicamento(CategoriaMedicamentoRequest categoriaMedicamentoRequest)
        {
            var categoria = new CategoriaMedicamento
            {
                Nombre = categoriaMedicamentoRequest.Nombre,
                Descripcion = categoriaMedicamentoRequest.Descripcion,
                Estado = true

            };
            await _repositorioCategoriaMedicamento.AddAsync(categoria);
            return new CategoriaMedicamentoResponse
            {
                Success = true,
                Message = "Categoria de medicamento agregada exitosamente."
            };
        }

        public async Task<CategoriaMedicamentoDto> UpdateCategoriaMedicamento(CategoriaMedicamentoRequest categoriaMedicamentoRequest)
        {
            var categoriaMedicamento = await _repositorioCategoriaMedicamento.GetById(categoriaMedicamentoRequest.Id)
                            ?? throw new NotFoundException("CategoriaMedicamento", categoriaMedicamentoRequest.Id);

            categoriaMedicamento.Nombre = categoriaMedicamentoRequest.Nombre;
            categoriaMedicamento.Descripcion = categoriaMedicamentoRequest.Descripcion;

            await _repositorioCategoriaMedicamento.UpdateAsync(categoriaMedicamento, categoriaMedicamentoRequest.Id);

            var categoriaMedicamentoDto = new CategoriaMedicamentoDto
            {
                Id = categoriaMedicamento.Id,
                Nombre = categoriaMedicamento.Nombre,
                Descripcion = categoriaMedicamento.Descripcion
            };

            return categoriaMedicamentoDto;
        }

        public async Task<ResultadoPaginado<CategoriaMedicamentoDto>> GetAllCategoriaMedicamento(CategoriaMedicamentoFiltros categoriaMedicamentoFiltros)
        {
            var resultado = await _repositorioCategoriaMedicamento.GetAllFiltrado(categoriaMedicamentoFiltros);
            var items = resultado.Items.Select(ct => new CategoriaMedicamentoDto
            {
                Id = ct.Id,
                Nombre = ct.Nombre,
                Descripcion = ct.Descripcion,
                CantidadMedicamentoAsignadoAunaCategoria = ct.Medicamentos.Count,
                Estado = ct.Estado,
            }).ToList();

            return new ResultadoPaginado<CategoriaMedicamentoDto>
            {
                Items = items,
                TotalRegistros = resultado.TotalRegistros,
                PaginaActual = resultado.PaginaActual,
                TamanoPagina = resultado.TamanoPagina
            }
            ;
        }

        public async Task DeleteCategoriaMedicamento(int id)
        {
           await _repositorioCategoriaMedicamento.InactivarCategoriaMedicamento(id);
        }

    }
}
