namespace CSweb.Models
{
    //Este modelo es el que se usa para la actividad del perfil
    public class PerfilActividadItemViewModel
    {
        // Esta parte es importantcicima ya que tiene su propia logica y muestra lo ultimo que ha pasado alrededor de los prompts
        // que se han publicado
        public string Tipo { get; set; } = string.Empty;
        public string ActorNombre { get; set; } = string.Empty;
        public string ActorUserName { get; set; } = string.Empty;
        public string TituloPrompt { get; set; } = string.Empty;
        public string Momento { get; set; } = string.Empty;
        public string? ExtractoComentario { get; set; }
    }
}

