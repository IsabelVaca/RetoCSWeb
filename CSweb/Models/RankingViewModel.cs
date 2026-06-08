namespace CSweb.Models;

public class RankingViewModel
{
    public List<UsuarioRankingViewModel> Usuarios { get; set; } = new();

    public UsuarioRankingViewModel? UsuarioActual { get; set; }

    public DetallePuntosViewModel? DetallePuntosUsuario { get; set; }

    public string TipoRanking { get; set; } = "general";

    public string Query { get; set; } = "";
}