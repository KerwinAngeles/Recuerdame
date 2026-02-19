namespace Recuerdame.Common
{
    public class ApiResponse<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Datos { get; set; }
        public IEnumerable<string>? Errores { get; set; }

        public static ApiResponse<T> Exitoso(T datos, string mensaje = "Operación exitosa") =>
            new() { Exito = true, Mensaje = mensaje, Datos = datos };

        public static ApiResponse<T> Fallido(string mensaje, IEnumerable<string>? errores = null) =>
            new() { Exito = false, Mensaje = mensaje, Errores = errores };
    }
}
