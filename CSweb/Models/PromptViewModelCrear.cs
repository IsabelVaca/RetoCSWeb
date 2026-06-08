namespace CSweb.Models;

public class PromptViewModelCrear
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Prompt { get; set; }
    public int IdCategoria { get; set; }
    public List<CategoriaPromptViewModel> Categorias { get; set; } = new List<CategoriaPromptViewModel>();
    public List<TipPromptViewModel> Tips { get; set; } = new List<TipPromptViewModel>();
    public List<ConsejoRapidoPromptViewModel> ConsejosRapidos { get; set; } = new List<ConsejoRapidoPromptViewModel>();
}