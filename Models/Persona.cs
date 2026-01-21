using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PersonasApi.Models
{
    [Table("personas")]
    public class Persona : BaseModel
    {
        [PrimaryKey("id", true)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }

        [Column("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Column("apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Column("numero_identificacion")]
        public string NumeroIdentificacion { get; set; } = string.Empty;

        [Column("tipo_identificacion")]
        public string TipoIdentificacion { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        // ❌ NO incluir columnas calculadas aquí
        // Se manejan en el DTO de lectura
    }
}