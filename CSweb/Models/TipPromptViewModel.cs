namespace CSweb.Models;

public class TipPromptViewModel
{
    public int IdTipPrompt { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public string? Ejemplo { get; set; }

    public int Orden { get; set; }

    public int Activo { get; set; }
}