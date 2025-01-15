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
    }
}
