namespace Recuerdame.Common
{
    public class ResultadoPaginado<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas => TamanoPagina > 0
            ? (int)Math.Ceiling(TotalRegistros / (double)TamanoPagina)
            : 0;
        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    }
}
