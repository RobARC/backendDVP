using System.ComponentModel.DataAnnotations;

namespace PersonasApi.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Pass { get; set; } = string.Empty;

        [Required]
        public int PersonaId { get; set; }
    }
}