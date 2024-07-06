namespace ControlboxLibreriaAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Libro
{
    [Key]
    public int LibroId { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Titulo { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Autor { get; set; }

    public string? Resumen { get; set; }

    public int CategoriaId { get; set; }

    public string? UrlImagen { get; set; }

    // Navegation Properties
    public Categoria? Categoria { get; set; }
    public ICollection<Resena>? Reseñas { get; set; }
}