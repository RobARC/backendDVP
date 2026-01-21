using System.ComponentModel.DataAnnotations;

namespace PersonasApi.Models.DTOs
{
    public class UpdatePersonaDTO
    {
        [StringLength(100)]
        public string? Nombres { get; set; }

        [StringLength(100)]
        public string? Apellidos { get; set; }

        [StringLength(50)]
        public string? NumeroIdentificacion { get; set; }

        [StringLength(20)]
        public string? TipoIdentificacion { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}