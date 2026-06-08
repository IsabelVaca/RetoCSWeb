using CSweb.Models;
namespace CSweb.Services;

// método que recibie el modelo del formulario (regresa true si el prompt se pudo guardar y false si hay un error)
public interface IPromptService
{
    Task<List<CategoriaPromptViewModel>> ObtenerCategoriasPrompt();

    Task<bool> CrearPrompt(PromptViewModelCrear prompt, int idUsuario);

    Task<List<TipPromptViewModel>> ObtenerTipsPrompt();

    Task<List<ConsejoRapidoPromptViewModel>> ObtenerConsejosRapidosPrompt();
}