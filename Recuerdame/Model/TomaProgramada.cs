using Recuerdame.Enums;

namespace Recuerdame.Model
{
    public class TomaProgramada
    {
        public int Id { get; set; }
        public int MedicamentoId { get; set; }
        public DateTime FechaHoraProgramada { get; set; }
        public DateTime FechaConfirmacion { get; set; }
        public EstadoToma EstadoToma { get; set; }
        public Medicamento Medicamento { get; set; }
    }
}
