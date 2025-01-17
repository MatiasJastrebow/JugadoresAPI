using PruebaAPI.Models;
using PruebaAPI.DTOs;
using PruebaAPI.Interfaces.ServicesInterfaces;
using PruebaAPI.Interfaces.ModelsInterfaces;

namespace PruebaAPI.Services
{
    public class JugadoresService : IJugadoresService
    {
        private static long lastId = 0; 
        private readonly List<IJugador> Jugadores;

        public JugadoresService()
        {
            this.Jugadores = new List<IJugador>
            {
                new Jugador (0, new DateOnly (2004, 5, 18), "Matias", 3)
            };
        }

        //GET
        public IList<IJugador> GetAll()
        {
            return this.Jugadores;
        }

        public IJugador GetById(long id)
        {
            return this.Jugadores.FirstOrDefault(j => j.id == id);
        }

        public IList<IJugador> GetMayoresA(int edad)
        {
            return this.Jugadores.FindAll(j => j.GetEdad() > edad);
        }

        public IList<IJugador> GetMayoresAFor(int edad)
        {
            List<IJugador> lista = new();
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].GetEdad() > edad) lista.Add(Jugadores[i]);
            }
            return lista;
        }

        public IList<String> GetJugadoresConMasExperienciaA(int anios)
        {
            List<IJugador> lista = this.Jugadores.FindAll(j => j.experienceInYears > anios);
            return lista.ConvertAll(j => j.name);
        }

        public IList<String> GetJugadoresConMasExperienciaAFor(int anios)
        {
            List<IJugador> listaJugadores = new();
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].experienceInYears > anios) listaJugadores.Add(Jugadores[i]);
            }
            return this.ObtenerNombres(listaJugadores);
        }

        public IList<IJugador> GetJugadoresCuyoNombreContieneA(string cadena)
        {
            return this.Jugadores.FindAll(j => j.name.Contains(cadena) && j.GetEdad() < 25 && j.GetEdad() > 18);
        }

        public IList<IJugador> GetJugadoresCuyoNombreContieneAFor(string cadena)
        {
            List<IJugador> lista = new();
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].GetEdad() > 18 && Jugadores[i].GetEdad() < 25 && Jugadores[i].NombreContieneCadena(cadena)) lista.Add(Jugadores[i]);
            }
            return lista;
        }

        //POST
        public IJugador Crear(JugadorDto jugadorDto)
        {
            if (jugadorDto.birthDate is not null && jugadorDto.name is not null && jugadorDto.experienceInYears is not null)
            {
                Jugador jugador = new(++lastId, jugadorDto.birthDate.GetValueOrDefault(), jugadorDto.name, jugadorDto.experienceInYears.GetValueOrDefault());
                this.Jugadores.Add(jugador);
                return jugador;
            }
            return null;
        }

        //PUT
        public IJugador Modificar(long id, JugadorDto jugadorDto)
        {
            var jugador = this.GetById(id);
            if (jugador is not null)
            {
                if (jugadorDto.birthDate.HasValue)
                    jugador.birthDate = jugadorDto.birthDate.GetValueOrDefault();
                if (jugadorDto.name is not null)
                    jugador.name = jugadorDto.name;
                if (jugadorDto.experienceInYears.HasValue)
                    jugador.experienceInYears = jugadorDto.experienceInYears.GetValueOrDefault();
                return jugador;
            }
            return null;
        }

        //DELETE
        public void Eliminar(long id)
        {
            var jugador = this.GetById(id);
            if (jugador is not null) this.Jugadores.Remove(jugador);
        }

        //Metodos auxiliares
        public List<String> ObtenerNombres (IList<IJugador> jugadores)
        {
            List<String> result = new();
            for (int i = 0; i < jugadores.Count; i++)
            {
                result.Add(jugadores[i].name);
            }
            return result;
        }
    }
}
