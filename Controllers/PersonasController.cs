using Microsoft.AspNetCore.Mvc;
using PersonasApi.Models.DTOs;
using PersonasApi.Services;

namespace PersonasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaService _personaService;

        public PersonasController(PersonaService personaService)
        {
            _personaService = personaService;
        }

        /// <summary>
        /// Obtiene todas las personas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaDTO>>> GetAll()
        {
            try
            {
                var personas = await _personaService.GetAllPersonasAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener personas", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una persona por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDTO>> GetById(int id)
        {
            try
            {
                var persona = await _personaService.GetPersonaByIdAsync(id);
                
                if (persona == null)
                    return NotFound(new { message = "Persona no encontrada" });

                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener persona", error = ex.Message });
            }
        }

        /// <summary>
        /// Crea una nueva persona
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PersonaDTO>> Create([FromBody] CreatePersonaDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var persona = await _personaService.CreatePersonaAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = persona.Id }, persona);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear persona", error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una persona existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaDTO>> Update(int id, [FromBody] UpdatePersonaDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var persona = await _personaService.UpdatePersonaAsync(id, updateDto);
                
                if (persona == null)
                    return NotFound(new { message = "Persona no encontrada" });

                return Ok(persona);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar persona", error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una persona
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _personaService.DeletePersonaAsync(id);
                
                if (!result)
                    return NotFound(new { message = "Persona no encontrada" });

                return Ok(new { message = "Persona eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar persona", error = ex.Message });
            }
        }
    }
}
