namespace PersonasApi.Models.DTOs
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public int PersonaId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public PersonaDTO? Persona { get; set; }
    }
}