namespace ControlboxLibreriaAPI.Entities;

using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

public class Resena
{
    [Key]
    public int ReseñaId { get; set; }

    [Required]
    public string? UsuarioFirebaseUserId { get; set; }

    [Required]
    public int LibroId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Calificacion { get; set; }

    [Required]
    public string? Comentario { get; set; }

    [Required]
    [System.Text.Json.Serialization.JsonIgnore]
    public DateTime FechaReseña { get; set; }

    // Navegation Properties
    public Usuario? Usuario { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Libro? Libro { get; set; }
}
