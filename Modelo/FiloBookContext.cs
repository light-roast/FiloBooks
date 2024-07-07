﻿using ControlboxLibreriaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlboxLibreriaAPI.Modelo
{
    public class FiloBookContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; } = null!;
        public DbSet<Libro> Libro { get; set; } = null!;
        public DbSet<Resena> Resena { get; set; } = null!;
        public DbSet<Categoria> Categoria { get; set; } = null!;

        public FiloBookContext(DbContextOptions<FiloBookContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=Database.db");
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Usuario>()
                .HasData(
                    new Usuario { UsuarioId = 1, NombreUsuario = "Daniel Echeverri LLano", Contraseña = "default_diffindb", CorreoElectronico = "echeverri121@gmail.com" }
                    
                );
        }
    }
}