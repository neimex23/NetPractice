using Microsoft.EntityFrameworkCore;
using NetPracticeCore.Models;

namespace NetPracticeCore.Data
{
    //<summary>
    // Repositorio para gestionar operaciones CRUD y búsquedas relacionadas con la entidad Confederación.
    //</summary>
    public interface IConfederacionRepository
    {
        //<summary>
        // Obtiene todas las confederaciones.
        //</summary>
        // <returns>Lista de confederaciones</returns>
        Task<List<Confederacion>> GetAll();

        //<summary>
        // Obtiene una confederación por su ID.
        //</summary>
        // <param name="id">ID de la confederación</param>
        Task<Confederacion> GetById(string id);

        //<summary>
        // Agrega una nueva confederación.
        //</summary>
        // <param name="conf">Objeto confederación a agregar</param>
        Task Add(Confederacion conf);

        //<summary>
        // Actualiza una confederación existente.
        //</summary>
        // <param name="conf">Objeto confederación con los datos actualizados</param
        Task Update(Confederacion conf);

        //<summary>
        // Elimina una confederación por su ID.
        //</summary>
        // <param name="id">ID de la confederación a eliminar</param>
        Task Delete(string id);

        //<summary>
        // Busca confederaciones por su nombre.
        // </summary>
        // <param name="texto">Texto a buscar en el nombre de la confederación
        Task<List<Confederacion>> Search(string texto);
    }

    public class ConfederacionRepository : IConfederacionRepository
    {
        private readonly AppDbContext _context;

        public ConfederacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Confederacion>> GetAll()
        {
            return await _context.Confederaciones.ToListAsync();
        }

        public async Task<Confederacion> GetById(string id)
        {
            return await _context.Confederaciones.FindAsync(id);
        }

        public async Task Add(Confederacion conf)
        {
            await _context.Confederaciones.AddAsync(conf);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Confederacion conf)
        {
            _context.Confederaciones.Update(conf);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var conf = await _context.Confederaciones.FindAsync(id);
            if (conf != null)
            {
                _context.Confederaciones.Remove(conf);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Confederacion>> Search(string texto)
        {
            return await _context.Confederaciones
                .Where(p => p.Nombre.Contains(texto))
                .ToListAsync();
        }
    }
}