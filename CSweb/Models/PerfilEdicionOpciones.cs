namespace CSweb.Models
{
    //Este modelo es el que se usa para los límites de edición del perfil
    public class PerfilEdicionOpciones
    {
        public int MaxNombre { get; set; } = 80;
        public int MaxUserName { get; set; } = 30;
        public int MaxBio { get; set; } = 500;

        //Extensiones de foto permitidas, con punto (ej. .jpg).
        public string[] ExtensionesFotoPermitidas { get; set; } = { ".jpg", ".jpeg", ".png", ".gif" };

        //Carpeta bajo wwwroot donde se guardan las fotos.
        public string CarpetaFotosRelativa { get; set; } = "imagenes/fotosperfil";
    }
}
