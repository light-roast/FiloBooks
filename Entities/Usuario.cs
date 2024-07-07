using System.ComponentModel.DataAnnotations;

namespace ControlboxLibreriaAPI.Entities
{
    public class Usuario
    {
        [Key]
        public string FirebaseUserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Resena>? Reseñas { get; set; }
    }
}
