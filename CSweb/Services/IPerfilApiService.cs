namespace CSweb.Services;

// Plantilla: llamadas HTTP del perfil contra Flask (GET perfil, PUT perfil unificado).
public interface IPerfilApiService
{
    Task<(List<Dictionary<string, object>> cabecera, List<Dictionary<string, object>> contenido)> ObtenerPerfilAsync(
        int idUsuario, string tab, int idUsuarioActual);

    // PUT /perfil/{id} — nombre, userName, bio y rutaFotoPerfil (opcional) en un solo cuerpo.
    Task<bool> ActualizarPerfilAsync(
        int idUsuario, string nombre, string userName, string bio, string? rutaFotoPerfil);
}
