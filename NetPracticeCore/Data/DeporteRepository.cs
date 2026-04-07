using Microsoft.EntityFrameworkCore;
using NetPracticeCore.Models;

namespace NetPracticeCore.Data
{
    //<summary>
    // Repositorio para gestionar operaciones CRUD y búsquedas relacionadas con la entidad Deporte.
    //</summary>
    public interface IDeporteRepository
    {
        //<summary>
        // Obtiene todos los deportes.
        //</summary>
        // <returns>Lista de deportes</returns>
        Task<List<Deporte>> GetAll();

        //<summary>
        // Obtiene un deporte por su ID.
        //</summary>
        // <param name="id">ID del deporte</param>
        Task<Deporte> GetById(string id);

        //<summary>
        // Agrega un nuevo deporte.
        //</summary>
        // <param name="deporte">Objeto deporte a agregar</param>
        Task Add(Deporte deporte);

        //<summary>
        // Actualiza un deporte existente.
        //</summary>
        // <param name="deporte">Objeto deporte con los datos actualizados</param>
        Task Update(Deporte deporte);

        //<summary>
        // Elimina un deporte por su ID.
        //</summary>
        // <param name="id">ID del deporte a eliminar</param>
        Task Delete(string id);

        //<summary>
        // Busca deportes por su nombre.
        // </summary>
        // <param name="texto">Texto a buscar en el nombre del deporte</param>
        Task<List<Deporte>> Search(string texto);
    }

    public class DeporteRepository : IDeporteRepository
    {
        private readonly AppDbContext _context;

        public DeporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Deporte>> GetAll()
        {
            return await _context.Deportes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Deporte> GetById(string id)
        {
            return await _context.Deportes.FindAsync(id);
        }

        public async Task Add(Deporte deporte)
        {
            await _context.Deportes.AddAsync(deporte);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Deporte deporte)
        {
            _context.Deportes.Update(deporte);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte != null)
            {
                _context.Deportes.Remove(deporte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Deporte>> Search(string texto)
        {
            return await _context.Deportes
                .Where(p => p.Nombre.Contains(texto))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}