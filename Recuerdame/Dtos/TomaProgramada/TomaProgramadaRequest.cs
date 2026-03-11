using Recuerdame.Enums;

namespace Recuerdame.Dtos.TomaProgramada
{
    public class TomaProgramadaRequest
    {
        public int Id { get; set; }
        public int MedicamentoId { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoToma EstadoToma { get; set; }
        public DateTime FechaConfirmacion { get; set; }
    }
}
