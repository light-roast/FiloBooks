namespace ControlboxLibreriaAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;

public class Resena
{
    [Key]
    public int ReseñaId { get; set; }

    [Required]
    public string? FirebaseUserId { get; set; }

    [Required]
    public int LibroId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Calificacion { get; set; }

    [Required]
    public string? Comentario { get; set; }

    [Required]
    public DateTime FechaReseña { get; set; }

    // Navegation Properties
    public Usuario? Usuario { get; set; }
    public Libro? Libro { get; set; }
}
