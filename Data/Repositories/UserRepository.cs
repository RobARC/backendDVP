using PersonasApi.Models;
using Supabase;

namespace PersonasApi.Data.Repositories
{
    public class UserRepository
    {
        private readonly Client _supabaseClient;

        public UserRepository(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<User?> GetByUsuarioAsync(string usuario)
        {
            var response = await _supabaseClient
                .From<User>()
                .Where(u => u.Usuario == usuario)
                .Single();

            return response;
        }

        public async Task<User> CreateAsync(User user)
        {
            // Usar RPC (stored procedure) para evitar problemas con id
            var parameters = new Dictionary<string, object>
            {
                { "p_persona_id", user.PersonaId },
                { "p_usuario", user.Usuario },
                { "p_pass", user.Pass }
            };

            var result = await _supabaseClient.Rpc("insert_usuario", parameters);
            
            // Parsear la respuesta como lista de Usuarios
            var usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(result.Content ?? "[]");
            return usuarios?.FirstOrDefault() ?? throw new Exception("Error al crear usuario");
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var response = await _supabaseClient
                .From<User>()
                .Where(u => u.Id == id)
                .Single();

            return response;
        }

        public async Task<User?> GetByPersonaIdAsync(int personaId)
        {
            var response = await _supabaseClient
                .From<User>()
                .Where(u => u.PersonaId == personaId)
                .Single();

            return response;
        }
    }
}
