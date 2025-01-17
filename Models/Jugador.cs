using PruebaAPI.Interfaces.ModelsInterfaces;

namespace PruebaAPI.Models
{
    public class Jugador: IJugador
    {
        public long id { get; private set; }
        public DateOnly birthDate {  get; set; }    
        public string name { get; set; }
        public int experienceInYears { get; set; }

        public Jugador (long _id, DateOnly _birthDate, string _name, int _experienceInYears)
        {
            this.id = _id;
            this.birthDate = _birthDate;
            this.name = _name;
            this.experienceInYears = _experienceInYears;
        }

        public int GetEdad()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - this.birthDate.Year;
            if (today < this.birthDate.AddYears(age)) age--;
            return age;
        }

        public Boolean NombreContieneCadena(string _string)
        {
            if (_string.Length > this.name.Length) return false;
            if (_string.Length.Equals(0)) return true; //Supongo que la cadena vacia siempre la contiene 
            for (var i = 0; i < this.name.Length; i++ )
            {
                var resultado = true;
                for (var j = 0; j < _string.Length; j++)
                {
                    if (this.name[i + j] != _string[j])
                    {
                        resultado = false;
                        break;
                    }
                }
                if (resultado) return true;
            }
            return false;
        }
    }
}
