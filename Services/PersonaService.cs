using PersonasApi.Data.Repositories;
using PersonasApi.Models;
using PersonasApi.Models.DTOs;

namespace PersonasApi.Services
{
    public class PersonaService
    {
        private readonly PersonaRepository _personaRepository;

        public PersonaService(PersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<PersonaDTO>> GetAllPersonasAsync()
        {
            var personas = await _personaRepository.GetAllAsync();
            return personas.Select(MapToDTO);
        }

        public async Task<PersonaDTO?> GetPersonaByIdAsync(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            return persona != null ? MapToDTO(persona) : null;
        }

        public async Task<PersonaDTO> CreatePersonaAsync(CreatePersonaDTO createDto)
        {
            // Validar que no exista con el mismo email
            var existingEmail = await _personaRepository.GetByEmailAsync(createDto.Email);
            if (existingEmail != null)
                throw new InvalidOperationException("Ya existe una persona con ese email");

            // Validar que no exista con el mismo número de identificación
            var existingId = await _personaRepository.GetByNumeroIdentificacionAsync(createDto.NumeroIdentificacion);
            if (existingId != null)
                throw new InvalidOperationException("Ya existe una persona con ese número de identificación");

            var persona = new Persona
            {
                Nombres = createDto.Nombres,
                Apellidos = createDto.Apellidos,
                NumeroIdentificacion = createDto.NumeroIdentificacion,
                TipoIdentificacion = createDto.TipoIdentificacion,
                Email = createDto.Email
            };

            var created = await _personaRepository.CreateAsync(persona);
            return MapToDTO(created);
        }

        public async Task<PersonaDTO?> UpdatePersonaAsync(int id, UpdatePersonaDTO updateDto)
        {
            var existing = await _personaRepository.GetByIdAsync(id);
            if (existing == null)
                return null;

            // Validar email si cambió
            if (!string.IsNullOrEmpty(updateDto.Email) && updateDto.Email != existing.Email)
            {
                var existingEmail = await _personaRepository.GetByEmailAsync(updateDto.Email);
                if (existingEmail != null)
                    throw new InvalidOperationException("Ya existe una persona con ese email");
            }

            // Validar número de identificación si cambió
            if (!string.IsNullOrEmpty(updateDto.NumeroIdentificacion) && updateDto.NumeroIdentificacion != existing.NumeroIdentificacion)
            {
                var existingId = await _personaRepository.GetByNumeroIdentificacionAsync(updateDto.NumeroIdentificacion);
                if (existingId != null)
                    throw new InvalidOperationException("Ya existe una persona con ese número de identificación");
            }

            // Actualizar solo los campos que no son null
            if (!string.IsNullOrEmpty(updateDto.Nombres))
                existing.Nombres = updateDto.Nombres;
            if (!string.IsNullOrEmpty(updateDto.Apellidos))
                existing.Apellidos = updateDto.Apellidos;
            if (!string.IsNullOrEmpty(updateDto.NumeroIdentificacion))
                existing.NumeroIdentificacion = updateDto.NumeroIdentificacion;
            if (!string.IsNullOrEmpty(updateDto.TipoIdentificacion))
                existing.TipoIdentificacion = updateDto.TipoIdentificacion;
            if (!string.IsNullOrEmpty(updateDto.Email))
                existing.Email = updateDto.Email;

            var updated = await _personaRepository.UpdateAsync(id, existing);
            return updated != null ? MapToDTO(updated) : null;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            var existing = await _personaRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _personaRepository.DeleteAsync(id);
        }

        private PersonaDTO MapToDTO(Persona persona)
        {
            return new PersonaDTO
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                NumeroIdentificacion = persona.NumeroIdentificacion,
                TipoIdentificacion = persona.TipoIdentificacion,
                Email = persona.Email,
                FechaCreacion = persona.FechaCreacion
                // NombreCompleto e IdentificacionCompleta se calculan automáticamente en el DTO
            };
        }
    }
}
