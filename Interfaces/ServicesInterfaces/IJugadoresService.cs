using PruebaAPI.DTOs;
using PruebaAPI.Interfaces.ModelsInterfaces;

namespace PruebaAPI.Interfaces.ServicesInterfaces
{
    public interface IJugadoresService
    {
        IList<IJugador> GetAll();
        IJugador GetById(long id);
        IList<IJugador> GetMayoresA(int edad);
        IList<IJugador> GetMayoresAFor(int edad);
        IList<string> GetJugadoresConMasExperienciaA(int anios);
        IList<string> GetJugadoresConMasExperienciaAFor(int anios);
        IList<IJugador> GetJugadoresCuyoNombreContieneA(string cadena);
        IList<IJugador> GetJugadoresCuyoNombreContieneAFor(string cadena);
        IJugador Crear(JugadorDto jugadorDto);
        IJugador Modificar(long id, JugadorDto jugadorDto);
        void Eliminar(long id);
    }
}
