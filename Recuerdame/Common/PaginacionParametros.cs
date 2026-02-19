namespace Recuerdame.Common
{
    public class PaginacionParametros
    {
        private const int MaxTamanoPagina = 50;
        private int _tamanoPagina = 10;

        public int Pagina { get; set; } = 1;

        public int TamanoPagina
        {
            get => _tamanoPagina;
            set => _tamanoPagina = value > MaxTamanoPagina ? MaxTamanoPagina : value;
        }
    }
}
