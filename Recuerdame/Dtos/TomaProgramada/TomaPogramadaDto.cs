using Recuerdame.Dtos.Medicamento;
using Recuerdame.Enums;

namespace Recuerdame.Dtos.TomaProgramada
{
    public class TomaPogramadaDto
    {
        public int Id { get; set; }
        public int MedicamentoId { get; set; }
        public DateTime FechaHoraProgramada { get; set; }
        public DateTime FechaConfirmacion { get; set; }
        public EstadoToma EstadoToma { get; set; }
        public string MedicamentoNombre { get; set; }
        public string CategoriaNombre { get; set; }
    }
}
