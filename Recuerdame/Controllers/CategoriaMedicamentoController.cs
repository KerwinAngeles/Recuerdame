using Microsoft.AspNetCore.Mvc;
using Recuerdame.Common;
using Recuerdame.Dtos.CategoriaMedicamento;
using Recuerdame.Services;

namespace Recuerdame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriaMedicamentoController : ControllerBase
    {
        private readonly ServicioCategoriaMedicamento _servicioCategoriaMedicamento;

        public CategoriaMedicamentoController(ServicioCategoriaMedicamento servicioCategoriaMedicamento)
        {
            _servicioCategoriaMedicamento = servicioCategoriaMedicamento;
        }

        /// <summary>
        /// Obtiene categorías con soporte de filtros y paginación.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ResultadoPaginado<CategoriaMedicamentoDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategorias([FromQuery] CategoriaMedicamentoFiltros filtros)
        {
            var resultado = await _servicioCategoriaMedicamento.GetAllCategoriaMedicamento(filtros);
            return Ok(ApiResponse<ResultadoPaginado<CategoriaMedicamentoDto>>.Exitoso(resultado));
        }

        /// <summary>
        /// Crea una nueva categoría de medicamento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CategoriaMedicamentoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategoria([FromBody] CategoriaMedicamentoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fallido(
                    "Datos de entrada inválidos.",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));

            var resultado = await _servicioCategoriaMedicamento.AddCategoriaMedicamento(request);
            return CreatedAtAction(
                nameof(GetCategorias),
                ApiResponse<CategoriaMedicamentoResponse>.Exitoso(resultado, "Categoría creada exitosamente."));
        }

        /// <summary>
        /// Actualiza los datos de una categoría existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<CategoriaMedicamentoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaMedicamentoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fallido(
                    "Datos de entrada inválidos.",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));

            request.Id = id;
            var resultado = await _servicioCategoriaMedicamento.UpdateCategoriaMedicamento(request);
            return Ok(ApiResponse<CategoriaMedicamentoDto>.Exitoso(resultado, "Categoría actualizada exitosamente."));
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            await _servicioCategoriaMedicamento.DeleteCategoriaMedicamento(id);
            return Ok(ApiResponse<CategoriaMedicamentoResponse>.Exitoso(null!, "Categoria eliminada exitosamente."));
        }
    }
}
