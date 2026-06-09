using System.Text;
using System.Text.Json;
using CSweb.Models;

namespace CSweb.Services;

public class PromptService : IPromptService
{
    private readonly HttpClient _httpClient;

    public PromptService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoriaPromptViewModel>> ObtenerCategoriasPrompt()
    {
        var url = "http://10.14.255.42:8001/categorias-prompt";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return new List<CategoriaPromptViewModel>();
        }

        var json = await response.Content.ReadAsStringAsync();

        var categorias = JsonSerializer.Deserialize<List<CategoriaPromptViewModel>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return categorias ?? new List<CategoriaPromptViewModel>();
    }

    public async Task<bool> CrearPrompt(PromptViewModelCrear prompt, int idUsuario)
    {
        var url = "http://10.14.255.42:8001/prompts";

        var promptApi = new
        {
            idUsuario = idUsuario,
            idCategoria = prompt.IdCategoria,
            titulo = prompt.Title,
            descripcion = prompt.Description,
            contenido = prompt.Prompt
        };

        var json = JsonSerializer.Serialize(promptApi);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<TipPromptViewModel>> ObtenerTipsPrompt()
    {
        var url = "http://10.14.255.42:8001/tips-prompt";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return new List<TipPromptViewModel>();
        }

        var json = await response.Content.ReadAsStringAsync();

        var tips = JsonSerializer.Deserialize<List<TipPromptViewModel>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return tips ?? new List<TipPromptViewModel>();
    }

    public async Task<List<ConsejoRapidoPromptViewModel>> ObtenerConsejosRapidosPrompt()
    {
        var url = "http://10.14.255.42:8001/consejos-rapidos-prompt";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return new List<ConsejoRapidoPromptViewModel>();
        }

        var json = await response.Content.ReadAsStringAsync();

        var consejos = JsonSerializer.Deserialize<List<ConsejoRapidoPromptViewModel>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return consejos ?? new List<ConsejoRapidoPromptViewModel>();
    }
}