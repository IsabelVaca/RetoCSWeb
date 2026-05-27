using CSweb.Models;

namespace CSweb.Services
{
    public interface IHomeApiService
    {
        Task<int> ObtenerUsuariosActivos();
        Task<List<HomeTendenciaViewModel>> ObtenerTendencias();
        Task<List<HomeCreadorViewModel>> ObtenerUsuariosSugeridos();
        Task<List<HomeActividadViewModel>> ObtenerActividadesRecientes();
        Task<int> ObtenerPromptsHoy();
        Task<int> ObtenerPromptsTotales();
    }
}