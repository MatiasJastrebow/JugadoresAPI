namespace PruebaAPI
{
    public class Jugador
    {
        private static long UltimoId = 0;

        public long Id { get; private set; }
        public DateOnly FechaNac {  get; set; }    
        public string Nombre { get; set; }
        public int AniosExperiencia { get; set; }

        public Jugador (DateOnly _FechaNac, string _Nombre, int _AniosExperiencia)
        {
            this.Id = ++UltimoId;
            this.FechaNac = _FechaNac;
            this.Nombre = _Nombre;
            this.AniosExperiencia = _AniosExperiencia;
        }

        public int GetEdad()
        {
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
            int edad = fechaActual.Year - this.FechaNac.Year;

            if (fechaActual < this.FechaNac.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }

        public Boolean NombreContiene(string cadena)
        {
            if (cadena.Length > this.Nombre.Length) return false;
            if (cadena.Length.Equals(0)) return true; //Supongo que la cadena vacia siempre la contiene 

            for (int i = 0; i < this.Nombre.Length; i++ )
            {
                bool resultado = true;
                for (int j = 0; j < cadena.Length; j++)
                {
                    if (this.Nombre[i + j] != cadena[j])
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
