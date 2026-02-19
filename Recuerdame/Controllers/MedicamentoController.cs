using Microsoft.AspNetCore.Mvc;
using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Services;

namespace Recuerdame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MedicamentoController : ControllerBase
    {
        private readonly ServicioMedicamento _servicioMedicamento;

        public MedicamentoController(ServicioMedicamento servicioMedicamento)
        {
            _servicioMedicamento = servicioMedicamento;
        }

        /// <summary>
        /// Obtiene medicamentos con soporte de filtros y paginación.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ResultadoPaginado<MedicamentoDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicamentos([FromQuery] MedicamentoFiltros filtros)
        {
            var resultado = await _servicioMedicamento.AllMedicamentos(filtros);
            return Ok(ApiResponse<ResultadoPaginado<MedicamentoDto>>.Exitoso(resultado));
        }

        /// <summary>
        /// Crea un nuevo medicamento y genera sus tomas programadas.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MedicamentoDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMedicamento([FromBody] MedicamentoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fallido(
                    "Datos de entrada inválidos.",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));

            var medicamento = await _servicioMedicamento.AddMedicamento(request);
            return CreatedAtAction(
                nameof(GetMedicamentos),
                ApiResponse<MedicamentoDto>.Exitoso(medicamento, "Medicamento creado exitosamente."));
        }

        /// <summary>
        /// Actualiza los datos de un medicamento existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<MedicamentoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMedicamento(int id, [FromBody] MedicamentoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fallido(
                    "Datos de entrada inválidos.",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));

            var medicamento = await _servicioMedicamento.UpdateMedicamento(id, request);
            return Ok(ApiResponse<MedicamentoDto>.Exitoso(medicamento, "Medicamento actualizado exitosamente."));
        }

        /// <summary>
        /// Inactiva un medicamento por su ID.
        /// </summary>
        [HttpPatch("{id:int}/inactivar")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InactivarMedicamento(int id)
        {
            await _servicioMedicamento.InactivarMedicamento(id);
            return Ok(ApiResponse<object>.Exitoso(null!, "Medicamento inactivado exitosamente."));
        }
    }
}
