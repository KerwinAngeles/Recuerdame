using Microsoft.AspNetCore.Mvc;
using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Services;

namespace Recuerdame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TomaProgramadaController : Controller
    {
       private readonly ServicioTomaProgramada _servicioTomaProgramada;
        public TomaProgramadaController(ServicioTomaProgramada servicioTomaProgramada)
        {
            _servicioTomaProgramada = servicioTomaProgramada;
        }

        /// <summary>
        /// Obtiene tomas programadas con soporte de filtros y paginación.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ResultadoPaginado<MedicamentoDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTomasProgramadas([FromQuery] TomaProgramadaFiltro filtros)
        {
            var resultado = await _servicioTomaProgramada.AllTomasProgramadas(filtros);
            return Ok(ApiResponse<ResultadoPaginado<TomaPogramadaDto>>.Exitoso(resultado));
        }
    }
}
