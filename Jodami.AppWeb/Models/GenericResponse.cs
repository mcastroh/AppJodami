namespace Jodami.AppWeb.Models
{
    public class GenericResponse<T> where T : class
    { 
        public bool Estado { get; set; }
        public string  Mensaje { get; set; }
        public T Objeto { get; set; }
    }
} 