namespace NetPractice.Models
{
    public class Pais
    {
        public int Id { get; private set; }

        public void setID(int id) { Id = id; }

        public string Nombre { get; set; }
        public DateTime FechaFundacion { get; set; }

        public int ConfederacionId { get; set; }
        public Confederacion Confederacion { get; set; }

        public int DeporteId { get; set; }
        public Deporte Deporte { get; set; }
    }
}
