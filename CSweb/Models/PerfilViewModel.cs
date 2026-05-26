namespace CSweb.Models
//Este modelo es el que se usa para el perfil del usuario..
{
    public class PerfilViewModel
    {
        // datos del perfil
        public string? ImagenPerfil { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;

        // Estos datos no se modifican en el perfil
        public string Correo { get; set; } = string.Empty;
        public int NumeroPublicaciones { get; set; }

        // lista de prompts publicados, guardados, likeados y actividad
        public List<PromptViewModel> Publicados { get; set; } = new();
        public List<PromptViewModel> Guardados { get; set; } = new();
        public List<PromptViewModel> Likeados { get; set; } = new();
        public List<PerfilActividadItemViewModel> Actividad { get; set; } = new();
    }
}
