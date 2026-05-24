namespace CSweb.Models
{
    //Este modelo es el que se usa para el formulario de edición del perfil
    public class PerfilEditarViewModel
    {
        // datos del perfil que se pueden editar
        public string Nombre { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        //ruta de la foto de perfil que se sube y se guarda en la base de datos
        public string? RutaFotoPerfil { get; set; }
        public IFormFile? FotoPerfil { get; set; }
    }  
}
