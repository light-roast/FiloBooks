namespace ControlboxLibreriaAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? NombreCategoria { get; set; }

    // Navegation Property
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Libro>? Libros { get; set; }
}

