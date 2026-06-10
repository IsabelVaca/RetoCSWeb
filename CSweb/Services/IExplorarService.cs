//define los metodos/acciones que se pueden realizar en la pagina de explorar mediante la API
//metodos que conectan la pagina con la API
using CSweb.Models;

namespace CSweb.Services
{
    public interface IExplorarApiService
    {
        Task<List<PromptViewModel>> ObtenerPromptsAsync(string query, int idUsuarioActual, string filtro);

        Task<LikeResponseViewModel?> ToggleLikeAsync(int idPrompt, int idUsuario);

        Task<GuardarResponseViewModel?> ToggleGuardarAsync(int idPrompt, int idUsuario);

        Task<ComentarioResponseViewModel?> PublicarComentarioAsync(int idPrompt, int idUsuario, string texto);}
}