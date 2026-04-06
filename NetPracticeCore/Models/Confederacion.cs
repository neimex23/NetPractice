namespace NetPracticeCore.Models
{
    public class Confederacion
    {
        public string Id { get; set; } 

        public Confederacion()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Nombre { get; set; }
    }
}