namespace Recuerdame.Excepciones
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensaje) : base(mensaje) { }

        public NotFoundException(string entidad, int id)
            : base($"{entidad} con ID {id} no fue encontrado.") { }
    }
}
