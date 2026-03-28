namespace NetPractice.Models
{
    public class Deporte
    {
        public int Id { get; private set; }

        public void setID(int id) { Id = id; }

        public string Nombre { get; set; }
    }
}
