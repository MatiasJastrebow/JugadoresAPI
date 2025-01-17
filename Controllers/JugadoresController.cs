using Microsoft.AspNetCore.Mvc;
using PruebaAPI.DTOs;
using PruebaAPI.Interfaces.ModelsInterfaces;
using PruebaAPI.Interfaces.ServicesInterfaces;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JugadoresController : ControllerBase
    {
        private readonly IJugadoresService jugadoresService;

        public JugadoresController(IJugadoresService _jugadoresService)
        {
            this.jugadoresService = _jugadoresService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IList<IJugador> jugadores = this.jugadoresService.GetAll();
            return Ok(jugadores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var jugador = this.jugadoresService.GetById(id);
            if (jugador is null) return NotFound();
            return Ok(jugador);
        }

        [HttpGet("mayores-a/{age}")]
        public IActionResult GetMayoresA(int age)
        {
            var jugadores = this.jugadoresService.GetMayoresA(age);
            return Ok(jugadores);
        }

        [HttpGet("anios-de-experiencia-mayor-a/{years}")]
        public IActionResult GetConMayorExperienciaA(int years)
        {
            var jugadores = this.jugadoresService.GetJugadoresConMasExperienciaAFor(years);
            return Ok(jugadores);
        }

        [HttpGet("entre-18-y-25-cuyo-nombre-contiene/{string}")]
        public IActionResult GetNombreContiene(string _string)
        {
            var jugadores = this.jugadoresService.GetJugadoresCuyoNombreContieneA(_string);
            return Ok(jugadores);
        }

        [HttpPost]
        public IActionResult Post([FromBody] JugadorDto jugadorDto)
        {
            if (jugadorDto.birthDate is not null && jugadorDto.name is not null  && jugadorDto.experienceInYears is not null)
            {
                var jugadorNuevo = jugadoresService.Crear(jugadorDto);
                if (jugadorNuevo is not null)
                    return CreatedAtAction(nameof(GetById), new { jugadorNuevo.id }, jugadorNuevo);
            }
            return BadRequest("No se completaron los datos necesarios");
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] JugadorDto jugadorDto)
        {
            var jugador = jugadoresService.Modificar(id, jugadorDto);
            if (jugador is null) return BadRequest("No existe un jugador con ese id");
            return Ok(jugador);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)   
        {
            jugadoresService.Eliminar(id);
            return NoContent();
        }

    }
}
