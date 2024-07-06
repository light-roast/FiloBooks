namespace ControlboxLibreriaAPI.Entities;

using Newtonsoft.Json;
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
    [System.Text.Json.Serialization.JsonIgnore]
    [JsonProperty("contraseña", Required = Required.Always)]

    public string? Contraseña { get; set; }

    [Required]
    [System.Text.Json.Serialization.JsonIgnore]
    [JsonProperty("rol", Required = Required.Always)]
    public string? Rol { get; set; }


    // Navegation Property
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Resena>? Reseñas { get; set; }
}

