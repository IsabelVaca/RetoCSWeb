using System.Text;
using System.Text.Json;

namespace CSweb.Services;

// Implementa IPerfilApiService: ejecuta GET/PUT a Flask (http://127.0.0.1:8001).
public class PerfilApiService : IPerfilApiService
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PerfilApiService(HttpClient http) => _http = http;

    public async Task<(List<Dictionary<string, object>> cabecera, List<Dictionary<string, object>> contenido)> ObtenerPerfilAsync(
        int idUsuario, string tab, int idUsuarioActual)
    {
        try
        {
            var url = $"perfil/{idUsuario}/{Uri.EscapeDataString(tab)}?idUsuarioActual={idUsuarioActual}";
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode)
                return ([], []);

            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var root = doc.RootElement;
            return (
                LeerLista(root, "cabecera"),
                LeerLista(root, "contenido")
            );
        }
        catch
        {
            return ([], []);
        }
    }

    public Task<bool> ActualizarPerfilAsync(int idUsuario, string nombre, string userName, string bio) =>
        PutAsync($"perfil/{idUsuario}", new { nombre, userName, bio });

    public Task<bool> ActualizarFotoAsync(int idUsuario, string rutaFotoPerfil) =>
        PutAsync($"perfil/{idUsuario}/foto", new { rutaFotoPerfil });

    private async Task<bool> PutAsync(string ruta, object cuerpo)
    {
        try
        {
            var json = JsonSerializer.Serialize(cuerpo, JsonOpts);
            var resp = await _http.PutAsync(ruta, new StringContent(json, Encoding.UTF8, "application/json"));
            return resp.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private static List<Dictionary<string, object>> LeerLista(JsonElement root, string prop) =>
        root.TryGetProperty(prop, out var arr) && arr.ValueKind == JsonValueKind.Array
            ? arr.EnumerateArray().Select(AObjeto).ToList()
            : [];

    private static Dictionary<string, object> AObjeto(JsonElement el)
    {
        var d = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        foreach (var p in el.EnumerateObject())
            d[p.Name] = AValor(p.Value);
        return d;
    }

    private static object AValor(JsonElement el) => el.ValueKind switch
    {
        JsonValueKind.String => el.GetString() ?? "",
        JsonValueKind.Number => el.TryGetInt64(out var n) ? n : el.GetDouble(),
        JsonValueKind.True => true,
        JsonValueKind.False => false,
        JsonValueKind.Array => el.EnumerateArray().Select(AObjeto).ToList(),
        JsonValueKind.Object => AObjeto(el),
        _ => ""
    };
}
