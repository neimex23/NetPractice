using Microsoft.EntityFrameworkCore;
using NetPracticeCore.Models;

namespace NetPracticeCore.Data
{
    //<summary>
    // Repositorio para gestionar operaciones CRUD y búsquedas relacionadas con la entidad País.
    //</summary>
    public interface IPaisRepository
    {
        //<summary>
        // Obtiene todos los países, incluyendo sus deportes y confederaciones relacionadas.
        //</summary>
        // <returns>Lista de países con sus relaciones</returns>
        Task<List<Pais>> GetAll();

        //<summary>
        // Obtiene un país por su ID, incluyendo sus deportes y confederaciones relacionadas.
        //</summary>
        // <param name="id">ID del país</param>
        Task<Pais> GetById(string id);

        //<summary>
        // Agrega un nuevo país a la base de datos.
        //</summary>
        // <param name="pais">Objeto país a agregar</param>
        Task Add(Pais pais);

        //<summary>
        // Actualiza un país existente en la base de datos.
        //</summary>
        // <param name="pais">Objeto país con los datos actualizados</param>
        Task Update(Pais pais);

        //<summary>
        // Elimina un país de la base de datos por su ID.
        //</summary>
        // <param name="id">ID del país a eliminar</param>
        Task Delete(string id);

        //<summary>
        // Busca países por su nombre, incluyendo sus deportes y confederaciones relacionadas.
        // </summary>
        // <param name="texto">Texto a buscar en el nombre del país</param>
        Task<List<Pais>> Search(string texto);
    }

    public class PaisRepository : IPaisRepository
    {
        private readonly AppDbContext _context;

        public PaisRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pais>> GetAll()
        {
            return await _context.Paises
                .Include(p => p.Confederacion)
                .Include(p => p.Deporte)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pais> GetById(string id)
        {
            return await _context.Paises
                .Include(p => p.Confederacion)
                .Include(p => p.Deporte)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Pais pais)
        {
            await _context.Paises.AddAsync(pais);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Pais pais)
        {
            _context.Paises.Update(pais);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais != null)
            {
                _context.Paises.Remove(pais);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Pais>> Search(string texto)
        {
            return await _context.Paises
                .Include(p => p.Confederacion)
                .Include(p => p.Deporte)
                .Where(p => p.Nombre.Contains(texto ?? ""))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}