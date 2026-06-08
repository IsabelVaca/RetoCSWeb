//estructura de respuesta que usa el back para avisar que paso al fornt y este ejecute los cambios visuales
namespace CSweb.Models
{
    //like
    public class LikeResponseViewModel
    {
        public int Ok { get; set; }

        public string Mensaje { get; set; } = "";

        public int IdPrompt { get; set; }

        public int IdUsuario { get; set; }

        public int LikedByMe { get; set; }

        public int Likes { get; set; }
    }

    //guardar
    public class GuardarResponseViewModel
    {
        public int Ok { get; set; }

        public string Mensaje { get; set; } = "";

        public int IdPrompt { get; set; }

        public int IdUsuario { get; set; }

        public int SavedByMe { get; set; }

        public int Saves { get; set; }
    }

    //comentario 
    public class ComentarioResponseViewModel
    {
        public int Ok { get; set; }

        public string Mensaje { get; set; } = "";

        public int IdPrompt { get; set; }

        public int IdComentario { get; set; }

        public int Comments { get; set; }
    }
}