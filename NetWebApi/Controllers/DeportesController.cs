using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;
using NetPracticeCore.Models.DTOs;

namespace NetPracticeApi.Controllers
{
    /// <summary>
    /// API para gestionar deportes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DeporteController : ControllerBase
    {
        private readonly IDeporteRepository _depRepo;

        public DeporteController(IDeporteRepository depRepo)
        {
            _depRepo = depRepo;
        }

        /// <summary>
        /// Obtiene todos los deportes.
        /// </summary>
        /// <response code="200">Lista de deportes</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Deporte>>> GetAll()
        {
            return Ok(await _depRepo.GetAll());
        }

        /// <summary>
        /// Obtiene un deporte por ID.
        /// </summary>
        /// <param name="id">ID del deporte</param>
        /// <response code="200">Deporte encontrado</response>
        /// <response code="404">No encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Deporte>> GetById(string id)
        {
            var deporte = await _depRepo.GetById(id);

            if (deporte == null)
                return NotFound();

            return Ok(deporte);
        }

        /// <summary>
        /// Crea un nuevo deporte.
        /// </summary>
        /// <param name="deporte">Datos del deporte</param>
        /// <response code="201">Deporte creado</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Deporte>> Create([FromBody] DtoDeportes deporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoDeporte = new Deporte
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = deporte.Nombre
            };

            await _depRepo.Add(nuevoDeporte);

            return CreatedAtAction(nameof(GetById), new { id = nuevoDeporte.Id }, nuevoDeporte);
        }

        /// <summary>
        /// Actualiza un deporte existente.
        /// </summary>
        /// <param name="id">ID del deporte</param>
        /// <param name="deporte">Datos actualizados</param>
        /// <response code="204">Actualizado correctamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">No encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] Deporte deporte)
        {
            if (id != deporte.Id)
                return BadRequest("El ID no coincide.");

            var existente = await _depRepo.GetById(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = deporte.Nombre;

            await _depRepo.Update(existente);

            return NoContent();
        }

        /// <summary>
        /// Elimina un deporte.
        /// </summary>
        /// <param name="id">ID del deporte</param>
        /// <response code="204">Eliminado correctamente</response>
        /// <response code="404">No encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var existente = await _depRepo.GetById(id);

            if (existente == null)
                return NotFound();

            await _depRepo.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Busca deportes por nombre.
        /// </summary>
        /// <param name="search">Texto de búsqueda</param>
        /// <response code="200">Resultados encontrados</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Deporte>>> Search([FromQuery] string search)
        {
            var result = await _depRepo.Search(search);
            return Ok(result);
        }
    }
}