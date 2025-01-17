namespace PruebaAPI.Interfaces.ModelsInterfaces
{
    public interface IJugador 
    {
        long id { get; }
        DateOnly birthDate { get; set; }
        string name { get; set; }
        int experienceInYears { get; set; }

        int GetEdad();
        bool NombreContieneCadena(string cadena);
    }
}
