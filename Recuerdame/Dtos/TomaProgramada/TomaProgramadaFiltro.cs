using Recuerdame.Common;
using Recuerdame.Enums;

namespace Recuerdame.Dtos.TomaProgramada
{
    public class TomaProgramadaFiltro : PaginacionParametros
    {
        public int Id { get; set; }
        public int? MedicamentoId { get; set; }
        public DateTime? FechaHoraProgramada { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public EstadoToma? EstadoToma { get; set; }
        public string OrdenarPor { get; set; } = "fechahoraprogramada";
        public bool OrdenAscendente { get; set; } = false;
    }
}
