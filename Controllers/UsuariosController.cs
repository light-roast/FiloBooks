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
using FirebaseAdmin.Auth;

namespace ControlboxLibreriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FirebaseAuth]
    public class UsuariosController : ControllerBase
    {
        private readonly FiloBookContext _context;

        public UsuariosController(FiloBookContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            if (!IsAuthorized(id))
            {
                return Forbid();
            }

            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (!IsAuthorized(id))
            {
                return Forbid();
            }

            if (id != usuario.FirebaseUserId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            if (!IsAuthorized(id))
            {
                return Forbid();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuario.Any(e => e.FirebaseUserId == id);
        }

        private bool IsAuthorized(string id)
        {
            var firebaseUser = HttpContext.Items["User"] as FirebaseToken;
            return firebaseUser != null && firebaseUser.Uid == id;
        }
    }
}