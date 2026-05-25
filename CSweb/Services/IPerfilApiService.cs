namespace CSweb.Services;

// Llamadas HTTP a la API Flask del perfil.
public interface IPerfilApiService
{
    Task<(List<Dictionary<string, object>> cabecera, List<Dictionary<string, object>> contenido)> ObtenerPerfilAsync(
        int idUsuario, string tab, int idUsuarioActual);

    Task<bool> ActualizarPerfilAsync(int idUsuario, string nombre, string userName, string bio);

    Task<bool> ActualizarFotoAsync(int idUsuario, string rutaFotoPerfil);
}
