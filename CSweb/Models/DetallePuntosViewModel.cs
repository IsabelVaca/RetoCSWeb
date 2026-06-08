namespace CSweb.Models
{
    public class DetallePuntosViewModel
    {
        public int IDUsuario { get; set; }
        public string UserName { get; set; } = "";

        public int LikesRecibidos { get; set; }
        public int GuardadosRecibidos { get; set; }
        public int ComentariosRecibidos { get; set; }

        public int TotalPuntos { get; set; }

        public int PuntosLikes => LikesRecibidos * 10;
        public int PuntosGuardados => GuardadosRecibidos * 10;
        public int PuntosComentarios => ComentariosRecibidos * 10;
    }
}

