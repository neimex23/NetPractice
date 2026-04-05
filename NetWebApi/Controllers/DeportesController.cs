using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetWebApi.Data;
using NetWebApi.Models;

namespace NetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeportesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeportesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deporte>>> GetDeportes()
        {
            return await _context.Deportes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deporte>> GetDeporte(string id)
        {
            var deporte = await _context.Deportes.FindAsync(id);

            if (deporte == null)
            {
                return NotFound();
            }

            return deporte;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeporte(string id, Deporte deporte)
        {
            if (id != deporte.Id)
            {
                return BadRequest();
            }

            _context.Entry(deporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeporteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Deporte>> PostDeporte(Deporte deporte)
        {
            _context.Deportes.Add(deporte);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeporteExists(deporte.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeporte", new { id = deporte.Id }, deporte);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeporte(string id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null)
            {
                return NotFound();
            }

            _context.Deportes.Remove(deporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeporteExists(string id)
        {
            return _context.Deportes.Any(e => e.Id == id);
        }
    }
}
