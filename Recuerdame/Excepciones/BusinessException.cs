namespace Recuerdame.Excepciones
{
    public class BusinessException : Exception
    {
        public BusinessException(string mensaje) : base(mensaje) { }
    }
}
