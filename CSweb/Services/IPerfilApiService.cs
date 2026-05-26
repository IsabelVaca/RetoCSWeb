namespace CSweb.Services;

// Plantilla: define qué llamadas HTTP hace el perfil contra Flask (GET perfil, PUT datos, PUT foto).
// PerfilApiService es la clase que las implementa de verdad..
public interface IPerfilApiService
{
    Task<(List<Dictionary<string, object>> cabecera, List<Dictionary<string, object>> contenido)> ObtenerPerfilAsync(
        int idUsuario, string tab, int idUsuarioActual);

    Task<bool> ActualizarPerfilAsync(int idUsuario, string nombre, string userName, string bio);

    Task<bool> ActualizarFotoAsync(int idUsuario, string rutaFotoPerfil);
}
