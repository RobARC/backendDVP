using PersonasApi.Models;
using Supabase;

namespace PersonasApi.Data.Repositories
{
    public class PersonaRepository
    {
        private readonly Client _supabaseClient;

        public PersonaRepository(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            var response = await _supabaseClient
                .From<Persona>()
                .Get();

            return response.Models;
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            var response = await _supabaseClient
                .From<Persona>()
                .Where(p => p.Id == id)
                .Single();

            return response;
        }

        public async Task<Persona> CreateAsync(Persona persona)
        {
            // Usar RPC (stored procedure) para evitar problemas con id
            var parameters = new Dictionary<string, object>
            {
                { "p_nombres", persona.Nombres },
                { "p_apellidos", persona.Apellidos },
                { "p_numero_identificacion", persona.NumeroIdentificacion },
                { "p_tipo_identificacion", persona.TipoIdentificacion },
                { "p_email", persona.Email }
            };

            var result = await _supabaseClient.Rpc("insert_persona", parameters);
            
            // Parsear la respuesta como lista de Personas
            var personas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Persona>>(result.Content ?? "[]");
            return personas?.FirstOrDefault() ?? throw new Exception("Error al crear persona");
        }

        public async Task<Persona?> UpdateAsync(int id, Persona persona)
        {
            persona.Id = id;
            
            var response = await _supabaseClient
                .From<Persona>()
                .Where(p => p.Id == id)
                .Update(persona);

            return response.Models.FirstOrDefault();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _supabaseClient
                .From<Persona>()
                .Where(p => p.Id == id)
                .Delete();

            return true;
        }

        public async Task<Persona?> GetByEmailAsync(string email)
        {
            var response = await _supabaseClient
                .From<Persona>()
                .Where(p => p.Email == email)
                .Single();

            return response;
        }

        public async Task<Persona?> GetByNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            var response = await _supabaseClient
                .From<Persona>()
                .Where(p => p.NumeroIdentificacion == numeroIdentificacion)
                .Single();

            return response;
        }
    }
}