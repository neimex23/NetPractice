namespace NetPractice.Models
{
    public class Deporte
    {
        public string Id { get; set; }

        public Deporte()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Nombre { get; set; }
    }
}
