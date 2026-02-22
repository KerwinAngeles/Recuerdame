using Recuerdame.Model;

namespace Recuerdame.Dtos.Medicamento
{
    public class MedicamentoRequest
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Dosis { get; set; }
        public int FrecuenciaHora { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Nota { get; set; }
    }
}
