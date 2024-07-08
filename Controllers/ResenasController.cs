using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlboxLibreriaAPI.Entities;
using ControlboxLibreriaAPI.Modelo;
using ControlboxLibreriaAPI.Filters;

namespace ControlboxLibreriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenasController : ControllerBase
    {
        private readonly FiloBookContext _context;

        public ResenasController(FiloBookContext context)
        {
            _context = context;
        }

        // GET: api/Resenas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resena>>> GetResena()
        {
            return await _context.Resena.ToListAsync();
        }

        // GET: api/Resenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resena>> GetResena(int id)
        {
            var resena = await _context.Resena.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            return resena;
        }

        // PUT: api/Resenas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [FirebaseAuth]
        public async Task<IActionResult> PutResena(int id, Resena resena)
        {
            if (id != resena.ReseñaId)
            {
                return BadRequest();
            }

            var userId = HttpContext.Items["FirebaseUserId"] as string;
            var existingResena = await _context.Resena.FindAsync(id);

            if (existingResena == null)
            {
                return NotFound();
            }

            if (existingResena.FirebaseUserId != userId)
            {
                return Forbid("You can only edit your own reviews.");
            }

            existingResena.Calificacion = resena.Calificacion;
            existingResena.Comentario = resena.Comentario;
            existingResena.FechaReseña = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResenaExists(id))
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

        // POST: api/Resenas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [FirebaseAuth]
        public async Task<ActionResult<Resena>> PostResena(Resena resena)
        {
            var userId = HttpContext.Items["FirebaseUserId"] as string;
            resena.FirebaseUserId = userId;
            resena.FechaReseña = DateTime.UtcNow;

            _context.Resena.Add(resena);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResena", new { id = resena.ReseñaId }, resena);
        }

        // DELETE: api/Resenas/5
        [HttpDelete("{id}")]
        [FirebaseAuth]
        public async Task<IActionResult> DeleteResena(int id)
        {
            var userId = HttpContext.Items["FirebaseUserId"] as string;
            var resena = await _context.Resena.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            if (resena.FirebaseUserId != userId)
            {
                return Forbid("You can only delete your own reviews.");
            }

            _context.Resena.Remove(resena);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResenaExists(int id)
        {
            return _context.Resena.Any(e => e.ReseñaId == id);
        }
    }
}
