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

            if (existingResena.UsuarioFirebaseUserId != userId)
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
            resena.FechaReseña = DateTime.UtcNow;

            _context.Resena.Add(resena);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResena", new { id = resena.ReseñaId }, resena);
        }

        [HttpGet("libro/{libroId}")]
        public async Task<ActionResult<IEnumerable<ResenaDto>>> GetResenasPorLibro(int libroId)
        {
            var resenas = await _context.Resena
                .Include(r => r.Usuario) // Cargar el usuario asociado a la resena
                .Where(r => r.LibroId == libroId)
                .OrderByDescending(r => r.FechaReseña) // Ordenar por fecha de reseña descendente (más reciente primero)
                .ToListAsync();

            var resenasDto = resenas.Select(r => new ResenaDto
            {
                ResenaId = r.ReseñaId,
                LibroId = r.LibroId,
                UsuarioId = r.UsuarioFirebaseUserId,
                Calificacion = r.Calificacion,
                Comentario = r.Comentario,
                FechaResena = r.FechaReseña,
                Usuario = new UsuarioDto
                {
                    FirebaseUserId = r.Usuario.FirebaseUserId,
                    CorreoElectronico = r.Usuario.CorreoElectronico,
                    Username = r.Usuario.Username
                }
            }).ToList();

            return resenasDto;
        }


        // DELETE: api/Resenas/5
        [HttpDelete("{id}")]
        [FirebaseAuth]
        public async Task<IActionResult> DeleteResena(int id)
        {
            var resena = await _context.Resena.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
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

public class ResenaDto
{
    public int ResenaId { get; set; }
    public int LibroId { get; set; }
    public string UsuarioId { get; set; }
    public int Calificacion { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaResena { get; set; }
    public UsuarioDto Usuario { get; set; }
}

public class UsuarioDto
{
    public string FirebaseUserId { get; set; }
    public string CorreoElectronico { get; set; }
    public string Username { get; set; }
}