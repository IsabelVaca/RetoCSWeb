using CSweb.Models;

namespace CSweb.Services
{
    public interface IUsuarioAPIService
    {
        Task<List<UsuarioRankingViewModel>> ObtenerUsuariosRanking();

        Task<List<UsuarioRankingViewModel>> ObtenerRankingLikes();

        Task<DetallePuntosViewModel?> ObtenerDetallePuntosUsuario(int idUsuario);
    }
}