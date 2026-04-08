using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;
using NetPracticeCore.Models.DTOs;

namespace NetPracticeApi.Controllers
{
    /// <summary>
    /// API para gestionar países.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PaisController : ControllerBase
    {
        private readonly IPaisRepository _paisRepo;

        public PaisController(IPaisRepository paisRepo)
        {
            _paisRepo = paisRepo;
        }

        /// <summary>
        /// Obtiene todos los países con su confederación y deporte.
        /// </summary>
        /// <response code="200">Lista de países</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pais>>> GetAll()
        {
            return Ok(await _paisRepo.GetAll());
        }

        /// <summary>
        /// Obtiene un país por ID, incluyendo su confederación y deporte.
        /// </summary>
        /// <param name="id">ID del país</param>
        /// <response code="200">País encontrado</response>
        /// <response code="404">No encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pais>> GetById(string id)
        {
            var pais = await _paisRepo.GetById(id);

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        /// <summary>
        /// Crea un nuevo país.
        /// </summary>
        /// <param name="pais">Datos del país</param>
        /// <response code="201">País creado</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pais>> Create([FromBody] DtoPais pais)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoPais = new Pais
            {
                Nombre = pais.Nombre,
                FechaFundacion = pais.FechaFundacion,
                DeporteId = pais.DeporteId,
                ConfederacionId = pais.ConfederacionId
            };

            await _paisRepo.Add(nuevoPais);

            return CreatedAtAction(nameof(GetById), new { id = nuevoPais.Id }, nuevoPais);
        }

        /// <summary>
        /// Actualiza un país existente.
        /// </summary>
        /// <param name="id">ID del país</param>
        /// <param name="pais">Datos actualizados</param>
        /// <response code="204">Actualizado correctamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">No encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] Pais pais)
        {
            if (id != pais.Id)
                return BadRequest("El ID no coincide.");

            var existente = await _paisRepo.GetById(id);

            if (existente == null)
                return NotFound();

            // Actualizamos solo campos necesarios
            existente.Nombre = pais.Nombre;
            existente.FechaFundacion = pais.FechaFundacion;
            existente.ConfederacionId = pais.ConfederacionId;
            existente.DeporteId = pais.DeporteId;

            await _paisRepo.Update(existente);

            return NoContent();
        }

        /// <summary>
        /// Elimina un país.
        /// </summary>
        /// <param name="id">ID del país</param>
        /// <response code="204">Eliminado correctamente</response>
        /// <response code="404">No encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var existente = await _paisRepo.GetById(id);

            if (existente == null)
                return NotFound();

            await _paisRepo.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Busca países por nombre.
        /// </summary>
        /// <param name="search">Texto de búsqueda</param>
        /// <response code="200">Resultados encontrados</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pais>>> Search([FromQuery] string search)
        {
            var result = await _paisRepo.Search(search);
            return Ok(result);
        }
    }
}