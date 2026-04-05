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

        public Deporte Deporte { get; set; }
        public Confederacion Confederacion { get; set; }


        //Usados para ejercicio 1 donde MVC se comporta raramente con los objetos relacionados,
        //no se pueden usar directamente los objetos relacionados, se deben usar sus Id's
        [NotMapped]
        public string DeporteId { get; set; }

        [NotMapped]
        public string ConfederacionId { get; set; }
    }
}
