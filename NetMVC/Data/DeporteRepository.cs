using NetPractice.Models;

namespace NetPractice.Data
{
    public interface IDeporteRepository
    {
        List<Deporte> GetAll();
        Deporte GetById(string id);
        void Add(Deporte deporte);
        void Update(Deporte deporte);
        void Delete(string id);

        List<Deporte> Search(string texto);
    }

    public class DeporteRepository : IDeporteRepository
    {
        private readonly AppDbContext _context;

        public DeporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Deporte> GetAll()
        {
            return _context.Deportes.ToList();
        }

        public Deporte GetById(string id)
        {
            return _context.Deportes.Find(id);
        }

        public void Add(Deporte deporte)
        {
            _context.Deportes.Add(deporte);
            _context.SaveChanges();
        }

        public void Update(Deporte deporte)
        {
            _context.Deportes.Update(deporte);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var deporte = _context.Deportes.Find(id);
            if (deporte != null)
            {
                _context.Deportes.Remove(deporte);
                _context.SaveChanges();
            }
        }

        public List<Deporte> Search(string texto)
        {
            return _context.Deportes
                .Where(p => p.Nombre.Contains(texto))
                .ToList();
        }
    }
}
