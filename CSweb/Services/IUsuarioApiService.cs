using CSweb.Models;

namespace CSweb.Services
{
    public interface IUsuarioAPIService
    {
        Task<(bool ok, int? idUsuario, string? mensaje)> LoginUsuarioAsync(string username, string contrasena);

        Task<List<UsuarioRankingViewModel>> ObtenerUsuariosRanking();

        Task<List<UsuarioRankingViewModel>> ObtenerRankingLikes();

        Task<DetallePuntosViewModel?> ObtenerDetallePuntosUsuario(int idUsuario);
    }
}