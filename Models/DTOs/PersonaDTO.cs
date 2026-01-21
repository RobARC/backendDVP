namespace PersonasApi.Models.DTOs
{
    public class PersonaDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string TipoIdentificacion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        
        // Propiedades calculadas en el DTO (generadas en C#)
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string IdentificacionCompleta => $"{TipoIdentificacion}-{NumeroIdentificacion}";
    }
}