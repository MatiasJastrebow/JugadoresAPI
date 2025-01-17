namespace PruebaAPI.DTOs
{
    public class JugadorDto
    {
        public DateOnly? birthDate { get; set; }
        public string name { get; set; }
        public int? experienceInYears { get; set; }

        public JugadorDto(DateOnly? _FechaNac, string _Nombre, int? _AniosExperiencia)
        {
            this.birthDate = _FechaNac;
            this.name = _Nombre;
            this.experienceInYears = _AniosExperiencia;
        }

        //Constructor sin argumentos para el POST y el PUT
        public JugadorDto() { }
    }
}
