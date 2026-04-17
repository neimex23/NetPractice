using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;
using NetPracticeCore.Models.DTOs;

namespace NetPracticeApi.Controllers
{
    /// <summary>
    /// API para gestionar confederaciones.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConfederacionController : ControllerBase
    {
        private readonly IConfederacionRepository _confRepo;

        public ConfederacionController(IConfederacionRepository confRepo)
        {
            _confRepo = confRepo;
        }

        /// <summary>
        /// Obtiene todas las confederaciones.
        /// </summary>
        /// <response code="200">Lista de confederaciones</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Confederacion>>> GetAll()
        {
            return Ok(await _confRepo.GetAll());
        }

        /// <summary>
        /// Obtiene una confederación por ID.
        /// </summary>
        /// <param name="id">ID de la confederación</param>
        /// <response code="200">Confederación encontrada</response>
        /// <response code="404">No encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Confederacion>> GetById(string id)
        {
            var conf = await _confRepo.GetById(id);

            if (conf == null)
                return NotFound();

            return Ok(conf);
        }

        /// <summary>
        /// Crea una nueva confederación.
        /// </summary>
        /// <param name="conf">Datos de la confederación</param>
        /// <response code="201">Confederación creada</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Confederacion>> Create([FromBody] DtoConfederacion conf)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaConf = new Confederacion
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = conf.Nombre
            };

            await _confRepo.Add(nuevaConf);

            return CreatedAtAction(nameof(GetById), new { id = nuevaConf.Id }, nuevaConf);
        }

        /// <summary>
        /// Actualiza una confederación existente.
        /// </summary>
        /// <param name="id">ID de la confederación</param>
        /// <param name="conf">Datos actualizados</param>
        /// <response code="204">Actualizada correctamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">No encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] Confederacion conf)
        {
            if (id != conf.Id)
                return BadRequest("El ID no coincide.");

            var existente = await _confRepo.GetById(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = conf.Nombre;

            await _confRepo.Update(existente);

            return NoContent();
        }

        /// <summary>
        /// Elimina una confederación.
        /// </summary>
        /// <param name="id">ID de la confederación</param>
        /// <response code="204">Eliminada correctamente</response>
        /// <response code="404">No encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var existente = await _confRepo.GetById(id);

            if (existente == null)
                return NotFound();

            await _confRepo.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Busca confederaciones por nombre.
        /// </summary>
        /// <param name="search">Texto de búsqueda</param>
        /// <response code="200">Resultados encontrados</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Confederacion>>> Search([FromQuery] string search)
        {
            var result = await _confRepo.Search(search);
            return Ok(result);
        }
    }
}