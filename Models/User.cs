using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PersonasApi.Models
{
    [Table("usuarios")]
    public class User : BaseModel
    {
        [PrimaryKey("id", true)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }

        [Column("persona_id")]
        public int PersonaId { get; set; }

        [Column("usuario")]
        public string Usuario { get; set; } = string.Empty;

        [Column("pass")]
        public string Pass { get; set; } = string.Empty;

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Navegación (opcional)
        [Reference(typeof(Persona))]
        public Persona? Persona { get; set; }
    }
}