
namespace CSweb.Models
{
    public class UsuarioRankingViewModel
    {
        public int IDUsuario { get; set; }
        public string NombreUsuario { get; set; } = "";
        public string UserName { get; set; } = "";
        public string RutaFotoPerfil { get; set; } = "";
        public int Puntos { get; set; }
        public int TotalLikes { get; set; }
        public int Posicion { get; set; }
    }
}