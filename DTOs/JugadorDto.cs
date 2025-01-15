namespace PruebaAPI.DTOs
{
    public class JugadorDto
    {
        public DateOnly? FechaNac { get; set; }
        public string? Nombre { get; set; }
        public int? AniosExperiencia { get; set; }

        public JugadorDto(DateOnly? _FechaNac, string? _Nombre, int? _AniosExperiencia)
        {
            this.FechaNac = _FechaNac;
            this.Nombre = _Nombre;
            this.AniosExperiencia = _AniosExperiencia;
        }

        //Constructor sin argumentos para el POST y el PUT
        public JugadorDto()
        {

        }
    }
}
