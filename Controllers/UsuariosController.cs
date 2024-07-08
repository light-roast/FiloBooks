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
          
        private bool IsAuthorized(string id)
        {
            var firebaseUser = HttpContext.Items["User"] as FirebaseToken;
            return firebaseUser != null && firebaseUser.Uid == id;
        }
    }
}