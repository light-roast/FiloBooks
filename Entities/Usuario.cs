namespace ControlboxLibreriaAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    [Required]
    [MaxLength(50)]
    public string? NombreUsuario { get; set; }

    [Required]
    [EmailAddress]
    public string? CorreoElectronico { get; set; }

    [Required]
    [JsonIgnore]
    public string? Contraseña { get; set; }

    public string? FotoPerfil { get; set; }

    // Navegation Property
    public ICollection<Reseña>? Reseñas { get; set; }
}

