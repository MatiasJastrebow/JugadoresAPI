using Microsoft.AspNetCore.Mvc;
using PruebaAPI.DTOs;
using PruebaAPI.Services;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JugadoresController : ControllerBase
    {
        private readonly JugadoresService jugadoresService;

        public JugadoresController(JugadoresService _jugadoresService)
        {
            this.jugadoresService = _jugadoresService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Jugador> jugadores = this.jugadoresService.GetAll();
            return Ok(jugadores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            Jugador? posibleJugador = this.jugadoresService.GetById(id);

            if (posibleJugador == null) return NotFound();

            return Ok(posibleJugador);
        }

        [HttpGet("mayores-a/{edad}")]
        public IActionResult GetMayoresA(int edad)
        {
            List<Jugador> jugadores = this.jugadoresService.GetMayoresA(edad);
            return Ok(jugadores);
        }

        [HttpGet("anios-de-experiencia-mayor-a/{anios}")]
        public IActionResult GetConMayorExperienciaA(int anios)
        {
            List<String> jugadores = this.jugadoresService.GetJugadoresConMasExperienciaAFor(anios);
            return Ok(jugadores);
        }

        [HttpGet("entre-18-y-25-cuyo-nombre-contiene/{cadena}")]
        public IActionResult GetNombreContiene(string cadena)
        {
            List<Jugador> jugadores = this.jugadoresService.GetJugadoresCuyoNombreContieneA(cadena);
            return Ok(jugadores);
        }

        [HttpPost]
        public IActionResult Post([FromBody] JugadorDto jugadorDto)
        {
            if (jugadorDto.FechaNac != null && jugadorDto.Nombre != null  && jugadorDto.AniosExperiencia != null)
            {
                Jugador? jugadorNuevo = jugadoresService.Crear(jugadorDto);
                
                if (jugadorNuevo != null)
                {
                    return CreatedAtAction(nameof(GetById), new { id = jugadorNuevo.Id }, jugadorNuevo);
                }
            }

            return BadRequest("No se completaron los datos necesarios");
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] JugadorDto jugadorDto)
        {
            Jugador? jugador = jugadoresService.Modificar(id, jugadorDto);
            if (jugador == null) return BadRequest("No existe un jugador con ese id");
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
