namespace Recuerdame.Dtos.CategoriaMedicamento
{
    public class CategoriaMedicamentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<string> NombreMedicamentos { get; set; }

    }
}
