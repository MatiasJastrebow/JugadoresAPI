using PruebaAPI.DTOs;

namespace PruebaAPI.Services
{
    public class JugadoresService
    {
        private readonly List<Jugador> Jugadores;

        public JugadoresService()
        {
            this.Jugadores = new List<Jugador>
            {
                new (new DateOnly (2004, 5, 18), "Matias", 3)
            };
        }

        public List<Jugador> GetAll()
        {
            return this.Jugadores;
        }

        public Jugador? GetById(long id)
        {
            return this.Jugadores.FirstOrDefault(j => j.Id == id);
        }

        public Jugador? Crear(JugadorDto jugadorDto)
        {

            if (jugadorDto.FechaNac != null && jugadorDto.Nombre != null && jugadorDto.AniosExperiencia != null)
            {
                Jugador jugador = new(jugadorDto.FechaNac.GetValueOrDefault(), jugadorDto.Nombre, jugadorDto.AniosExperiencia.GetValueOrDefault());
                this.Jugadores.Add(jugador);
                return jugador;
            }

            return null;
        }

        public void Eliminar(long id)
        {
            Jugador? jugador = this.GetById(id); 

            if (jugador != null)
            {
                this.Jugadores.Remove(jugador);
            }
        }

        public Jugador? Modificar(long id, JugadorDto jugadorDto)
        {
            Jugador? jugador = this.GetById(id);

            if (jugador != null)
            {
                if (jugadorDto.FechaNac.HasValue)
                {
                    jugador.FechaNac = jugadorDto.FechaNac.GetValueOrDefault();
                }

                if (jugadorDto.Nombre != null)
                {
                    jugador.Nombre = jugadorDto.Nombre;
                }

                if (jugadorDto.AniosExperiencia.HasValue)
                {
                    jugador.AniosExperiencia = jugadorDto.AniosExperiencia.GetValueOrDefault();
                }

                return jugador;
            }

            return null;
        }

    }
}
