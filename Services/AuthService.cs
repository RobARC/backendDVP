using PersonasApi.Data.Repositories;
using PersonasApi.Models;
using PersonasApi.Models.DTOs;
using BCrypt.Net;

namespace PersonasApi.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly PersonaRepository _personaRepository;

        public AuthService(UserRepository userRepository, PersonaRepository personaRepository)
        {
            _userRepository = userRepository;
            _personaRepository = personaRepository;
        }

        public async Task<UserDTO?> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetByUsuarioAsync(loginDto.Usuario);
            
            if (user == null)
                return null;

            // Verificar contraseña
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Pass, user.Pass))
                return null;

            // Obtener datos de la persona
            var persona = await _personaRepository.GetByIdAsync(user.PersonaId);

            return new UserDTO
            {
                Id = user.Id,
                PersonaId = user.PersonaId,
                Usuario = user.Usuario,
                FechaCreacion = user.FechaCreacion,
                Persona = persona != null ? new PersonaDTO
                {
                    Id = persona.Id,
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    NumeroIdentificacion = persona.NumeroIdentificacion,
                    TipoIdentificacion = persona.TipoIdentificacion,
                    Email = persona.Email,
                    FechaCreacion = persona.FechaCreacion
                    // NombreCompleto e IdentificacionCompleta se calculan automáticamente
                } : null
            };
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDto)
        {
            // Validar que el usuario no exista
            var existingUser = await _userRepository.GetByUsuarioAsync(registerDto.Usuario);
            if (existingUser != null)
                throw new InvalidOperationException("El usuario ya existe");

            // Validar que la persona exista
            var persona = await _personaRepository.GetByIdAsync(registerDto.PersonaId);
            if (persona == null)
                throw new InvalidOperationException("La persona no existe");

            // Validar que la persona no tenga ya un usuario
            var existingPersonaUser = await _userRepository.GetByPersonaIdAsync(registerDto.PersonaId);
            if (existingPersonaUser != null)
                throw new InvalidOperationException("Esta persona ya tiene un usuario registrado");

            // Hashear contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Pass);

            var user = new User
            {
                PersonaId = registerDto.PersonaId,
                Usuario = registerDto.Usuario,
                Pass = passwordHash
            };

            var created = await _userRepository.CreateAsync(user);

            return new UserDTO
            {
                Id = created.Id,
                PersonaId = created.PersonaId,
                Usuario = created.Usuario,
                FechaCreacion = created.FechaCreacion,
                Persona = new PersonaDTO
                {
                    Id = persona.Id,
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    NumeroIdentificacion = persona.NumeroIdentificacion,
                    TipoIdentificacion = persona.TipoIdentificacion,
                    Email = persona.Email,
                    FechaCreacion = persona.FechaCreacion
                    // NombreCompleto e IdentificacionCompleta se calculan automáticamente
                }
            };
        }
    }
}
