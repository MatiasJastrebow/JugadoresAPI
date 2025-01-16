using System.Collections.Generic;
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

        //GET
        public List<Jugador> GetAll()
        {
            return this.Jugadores;
        }

        public Jugador? GetById(long id)
        {
            return this.Jugadores.FirstOrDefault(j => j.Id == id);
        }

        public List<Jugador> GetMayoresA(int edad)
        {
            return this.Jugadores.FindAll(j => j.GetEdad() > edad);
        }

        public List<Jugador> GetMayoresAFor(int edad)
        {
            List<Jugador> lista = new();

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].GetEdad() > edad) lista.Add(Jugadores[i]);
            }

            return lista;
        }

        public List<String> GetJugadoresConMasExperienciaA(int anios)
        {
            List<Jugador> lista = this.Jugadores.FindAll(j => j.AniosExperiencia > anios);
            return lista.ConvertAll(j => j.Nombre);
        }

        public List<String> GetJugadoresConMasExperienciaAFor(int anios)
        {
            List<Jugador> listaJugadores = new();

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].AniosExperiencia > anios) listaJugadores.Add(Jugadores[i]);
            }

            return this.ObtenerNombres(listaJugadores);
        }

        public List<Jugador> GetJugadoresCuyoNombreContieneA(string cadena)
        {
            return this.Jugadores.FindAll(j => j.Nombre.Contains(cadena) && j.GetEdad() < 25 && j.GetEdad() > 18);
        }

        public List<Jugador> GetJugadoresCuyoNombreContieneAFor(string cadena)
        {
            List<Jugador> lista = new();

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].GetEdad() > 18 && Jugadores[i].GetEdad() < 25 && Jugadores[i].NombreContiene(cadena)) lista.Add(Jugadores[i]);
            }

            return lista;
        }

        //POST
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

        //PUT
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

        //DELETE
        public void Eliminar(long id)
        {
            Jugador? jugador = this.GetById(id);

            if (jugador != null)
            {
                this.Jugadores.Remove(jugador);
            }
        }


        //Metodos auxiliares
        public List<String> ObtenerNombres (List<Jugador> jugadores)
        {
            List<String> result = new();
            for (int i = 0; i < jugadores.Count; i++)
            {
                result.Add(jugadores[i].Nombre);
            }
            return result;
        }
    }
}
