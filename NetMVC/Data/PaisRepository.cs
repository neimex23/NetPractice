using Microsoft.EntityFrameworkCore;
using NetPractice.Models;

namespace NetPractice.Data
{
    //<summary>
    // Interfaz para el repositorio de países, define los métodos para realizar operaciones CRUD y búsquedas.
    //</summary>
    public interface IPaisRepository
    {
        //<summary>
        // Obtiene una lista de todos los países, incluyendo sus confederaciones y deportes relacionados.
        //</summary>
        List<Pais> GetAll();

        //<summary>
        // Obtiene un país por su ID, incluyendo su confederación y deporte relacionados.
        //</summary>
        Pais GetById(string id);

        //<summary>
        // Agrega un nuevo país a la base de datos.
        //</summary>
        void Add(Pais pais);

        //<summary>
        // Actualiza un país existente en la base de datos.
        //</summary>
        void Update(Pais pais);

        //<summary>
        // Elimina un país de la base de datos por su ID.
        //</summary>
        void Delete(string id);

        //<summary>
        // Busca países cuyo nombre contenga el texto especificado, devolviendo una lista de resultados.
        //</summary>
        List<Pais> Search(string texto);
    }

    public class PaisRepository : IPaisRepository
    {
        private readonly AppDbContext _context;

        public PaisRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Pais> GetAll()
        {
            return _context.Paises
                .Include(p => p.Confederacion)
                .Include(p => p.Deporte)
                .ToList();
        }

        public Pais GetById(string id)
        {
            return _context.Paises
                .Include(p => p.Confederacion)
                .Include(p => p.Deporte)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Add(Pais pais)
        {
            _context.Paises.Add(pais);
            _context.SaveChanges();
        }

        public void Update(Pais pais)
        {
            _context.Paises.Update(pais);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var pais = _context.Paises.Find(id);
            if (pais != null)
            {
                _context.Paises.Remove(pais);
                _context.SaveChanges();
            }
        }

        public List<Pais> Search(string texto)
        {
            return _context.Paises
                .Where(p => p.Nombre.Contains(texto))
                .ToList();
        }
    }
}
