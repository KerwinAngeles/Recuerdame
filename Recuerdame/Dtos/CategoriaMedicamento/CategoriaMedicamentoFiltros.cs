using Recuerdame.Common;

namespace Recuerdame.Dtos.CategoriaMedicamento
{
    public class CategoriaMedicamentoFiltros : PaginacionParametros
    {
        public string? Nombre { get; set; }
        public string OrdenarPor { get; set; } = "nombre";
        public bool OrdenAscendente { get; set; } = true;
    }
}
