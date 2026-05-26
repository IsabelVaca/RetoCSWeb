using CSweb.Models;
using CSweb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSweb.ViewComponents;

// Carga userName desde la API de perfil para la pastilla del layout.
public class UsuarioNavbarViewComponent : ViewComponent
{
    private readonly IPerfilApiService _perfilApi;

    public UsuarioNavbarViewComponent(IPerfilApiService perfilApi) => _perfilApi = perfilApi;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var id = HttpContext.Session.GetInt32("UsuarioId") ?? 1;
        if (HttpContext.Session.GetInt32("UsuarioId") is null)
            HttpContext.Session.SetInt32("UsuarioId", id);

        var vm = new UsuarioNavbarViewModel();
        var (cab, _) = await _perfilApi.ObtenerPerfilAsync(id, "publicados", id);
        if (cab.Count > 0)
        {
            var user = Leer(cab[0], "userName");
            if (!string.IsNullOrWhiteSpace(user))
            {
                vm.UserName = user.Trim();
                vm.Iniciales = Iniciales(vm.UserName);
            }
        }

        return View(vm);
    }

    private static string Leer(Dictionary<string, object> d, string clave) =>
        d.TryGetValue(clave, out var v) ? v?.ToString() ?? "" : "";

    // Primeras 2 letras del userName para el círculo (ej. jimmy → JI).
    private static string Iniciales(string userName)
    {
        var u = userName.Trim();
        if (u.Length == 0) return "";
        return u.Length >= 2 ? u[..2].ToUpperInvariant() : u.ToUpperInvariant();
    }
}
