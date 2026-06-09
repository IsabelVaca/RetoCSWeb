using CSweb.Models;

namespace CSweb.Services
{
    public interface ITiendaApiService
    {
        Task<List<ItemTiendaModel>> ObtenerItemsTienda(int idUsuario);
        Task<SaldoCobaltsModel> ObtenerSaldoCobalts(int idUsuario);
        Task<CompraResultadoModel> ComprarItem(CompraRequestModel request);
    }
}