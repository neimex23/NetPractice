using System.ComponentModel.DataAnnotations.Schema;

namespace NetPractice.Models
{
    public class Pais
    {
        public string Id { get; private set; }

        public Pais()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Nombre { get; set; }
        public DateTime FechaFundacion { get; set; }

        public string DeporteId { get; set; }
        public Deporte Deporte { get; set; }

        public string ConfederacionId { get; set; }
        public Confederacion Confederacion { get; set; }
    }
}
