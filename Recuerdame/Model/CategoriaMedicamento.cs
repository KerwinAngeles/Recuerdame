namespace Recuerdame.Model
{
    public class CategoriaMedicamento : Auditoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public bool RequiereReceta { get; set; }
        public bool Estado { get; set; }
        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}
