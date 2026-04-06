using NetPracticeCore.Models;

namespace NetPracticeCore.Data
{
    public interface IConfederacionRepository
    {
        List<Confederacion> GetAll();
        Confederacion GetById(string id);
        void Add(Confederacion conf);
        void Update(Confederacion conf);
        void Delete(string id);
        List<Confederacion> Search(string texto);
    }

    public class ConfederacionRepository : IConfederacionRepository
    {
        private readonly AppDbContext _context;

        public ConfederacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Confederacion> GetAll()
        {
            return _context.Confederaciones.ToList();
        }

        public Confederacion GetById(string id)
        {
            return _context.Confederaciones.Find(id);
        }

        public void Add(Confederacion conf)
        {
            _context.Confederaciones.Add(conf);
            _context.SaveChanges();
        }

        public void Update(Confederacion conf)
        {
            _context.Confederaciones.Update(conf);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var conf = _context.Confederaciones.Find(id);
            if (conf != null)
            {
                _context.Confederaciones.Remove(conf);
                _context.SaveChanges();
            }
        }

        public List<Confederacion> Search(string texto)
        {
            return _context.Confederaciones
                .Where(p => p.Nombre.Contains(texto))
                .ToList();
        }
    }
}
