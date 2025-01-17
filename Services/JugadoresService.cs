using PruebaAPI.Models;
using PruebaAPI.DTOs;
using PruebaAPI.Interfaces.ServicesInterfaces;
using PruebaAPI.Interfaces.ModelsInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace PruebaAPI.Services
{
    public class JugadoresService : IJugadoresService
    {

        private readonly IMemoryCache _memoryCache;
        private long lastId;
        private readonly List<IJugador> jugadores;

        public JugadoresService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            if (!_memoryCache.TryGetValue("jugadores", out List<IJugador> _jugadores))
            {
                _jugadores = new List<IJugador>
                {
                    new Jugador(0, new DateOnly(2004, 5, 18), "Matias", 3)
                };
                _memoryCache.Set("jugadores", _jugadores);
            }
            if (!_memoryCache.TryGetValue("lastId", out long _lastId))
            {
                _lastId = 0; 
                _memoryCache.Set("lastId", _lastId);
            }
            this.jugadores = _memoryCache.Get<List<IJugador>>("jugadores");
            this.lastId = _memoryCache.Get<long>("lastId");
        }

        public JugadoresService()
        {
            this.jugadores = new List<IJugador>
            {
                new Jugador (0, new DateOnly (2004, 5, 18), "Matias", 3)
            };
        }

        //GET
        public IList<IJugador> GetAll()
        {
            return this.jugadores;
        }

        public IJugador GetById(long id)
        {
            return this.jugadores.FirstOrDefault(j => j.id == id);
        }

        public IList<IJugador> GetMayoresA(int edad)
        {
            return this.jugadores.FindAll(j => j.GetEdad() > edad);
        }

        public IList<IJugador> GetMayoresAFor(int edad)
        {
            List<IJugador> lista = new();
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].GetEdad() > edad) lista.Add(jugadores[i]);
            }
            return lista;
        }

        public IList<String> GetJugadoresConMasExperienciaA(int anios)
        {
            List<IJugador> lista = this.jugadores.FindAll(j => j.experienceInYears > anios);
            return lista.ConvertAll(j => j.name);
        }

        public IList<String> GetJugadoresConMasExperienciaAFor(int anios)
        {
            List<IJugador> listaJugadores = new();
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].experienceInYears > anios) listaJugadores.Add(jugadores[i]);
            }
            return this.ObtenerNombres(listaJugadores);
        }

        public IList<IJugador> GetJugadoresCuyoNombreContieneA(string cadena)
        {
            return this.jugadores.FindAll(j => j.name.Contains(cadena) && j.GetEdad() < 25 && j.GetEdad() > 18);
        }

        public IList<IJugador> GetJugadoresCuyoNombreContieneAFor(string cadena)
        {
            List<IJugador> lista = new();
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].GetEdad() > 18 && jugadores[i].GetEdad() < 25 && jugadores[i].NombreContieneCadena(cadena)) lista.Add(jugadores[i]);
            }
            return lista;
        }

        //POST
        public IJugador Crear(JugadorDto jugadorDto)
        {
            if (jugadorDto.birthDate is not null && jugadorDto.name is not null && jugadorDto.experienceInYears is not null)
            {
                Jugador jugador = new(++lastId, jugadorDto.birthDate.GetValueOrDefault(), jugadorDto.name, jugadorDto.experienceInYears.GetValueOrDefault());
                this.jugadores.Add(jugador);
                _memoryCache.Set("lastId", this.lastId);
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
                _memoryCache.Set("jugadores", this.jugadores);
                return jugador;
            }
            return null;
        }

        //DELETE
        public void Eliminar(long id)
        {
            var jugador = this.GetById(id);
            if (jugador is not null) 
            { 
                this.jugadores.Remove(jugador);
                _memoryCache.Set("jugadores", this.jugadores);
            }
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
