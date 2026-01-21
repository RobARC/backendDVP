using System.ComponentModel.DataAnnotations;

namespace PersonasApi.Models.DTOs
{
    public class CreatePersonaDTO
    {
        [Required(ErrorMessage = "Los nombres son requeridos")]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de identificación es requerido")]
        [StringLength(50)]
        public string NumeroIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        [StringLength(20)]
        public string TipoIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;
    }
}