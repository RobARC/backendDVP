using System.ComponentModel.DataAnnotations;

namespace PersonasApi.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Pass { get; set; } = string.Empty;
    }
}