using NetPractice.Models;

namespace NetPractice.Data
{
    /// <summary>
    /// Usado para Ejercicio 1 donde se pide una implementación sin base de datos, solo con listas en memoria.
    /// </summary>

    public static class DataStore
    {
        public static List<Confederacion> Confederaciones = new List<Confederacion>();
        public static List<Deporte> Deportes = new List<Deporte>();
        public static List<Pais> Paises = new List<Pais>();
    }
}
