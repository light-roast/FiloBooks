﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlboxLibreriaAPI.Entities;
using ControlboxLibreriaAPI.Modelo;

namespace ControlboxLibreriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly FiloBookContext _context;

        public LibrosController(FiloBookContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibro()
        {
            return await _context.Libro
                                 .Include(l => l.Categoria)
                                 .ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libro
                                      .Include(l => l.Categoria)
                                      .Include(l => l.Reseñas)
                                      .FirstOrDefaultAsync(l => l.LibroId == id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        //// PUT: api/Libros/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLibro(int id, Libro libro)
        //{
        //    if (id != libro.LibroId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(libro).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LibroExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Libros
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        //{
        //    _context.Libro.Add(libro);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLibro", new { id = libro.LibroId }, libro);
        //}

        //// DELETE: api/Libros/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLibro(int id)
        //{
        //    var libro = await _context.Libro.FindAsync(id);
        //    if (libro == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Libro.Remove(libro);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool LibroExists(int id)
        //{
        //    return _context.Libro.Any(e => e.LibroId == id);
        //}
    }
}
