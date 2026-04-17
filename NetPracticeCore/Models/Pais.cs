using System.ComponentModel.DataAnnotations.Schema;

namespace NetPracticeCore.Models
{
    public class Pais
    {
        public string Id { get; set; }

        public string Nombre { get; set; }
        public DateTime FechaFundacion { get; set; }

        public string DeporteId { get; set; }
        public Deporte? Deporte { get; set; }

        public string ConfederacionId { get; set; }
        public Confederacion? Confederacion { get; set; }
       
    }
}
