namespace MarcasAutos.Domain.Entities;

public class MarcaAuto
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string PaisOrigen { get; private set; }

    // Requerido por EF Core
    protected MarcaAuto() { }

    public MarcaAuto(string nombre, string paisOrigen)
    {
        Nombre = nombre;
        PaisOrigen = paisOrigen;
    }
}
