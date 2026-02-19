using Recuerdame.Common;

namespace Recuerdame.Dtos.Medicamento
{
    public class MedicamentoFiltros : PaginacionParametros
    {
        public string? Nombre { get; set; }
        public int? CategoriaId { get; set; }
        public bool? EstaActivo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string OrdenarPor { get; set; } = "nombre";
        public bool OrdenAscendente { get; set; } = true;
    }
}
