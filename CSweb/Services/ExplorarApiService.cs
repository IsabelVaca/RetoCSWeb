// logica de conexion con la API
// se hacen peticiones HTTP como GET, PUT y POST, y se convierten las respuestas JSON en modelos de C# para usarlo como objeto 
using System.Text;
using System.Text.Json;
using CSweb.Models;

namespace CSweb.Services
{
    public class ExplorarApiService : IExplorarApiService
    {
        private readonly HttpClient _httpClient;

        // gets a la api
        public ExplorarApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PromptViewModel>> ObtenerPromptsAsync(string query, int idUsuarioActual, string filtro)
        {
            if (query == null)
            {
                query = "";
            }

            if (filtro == null || filtro == "")
            {
                filtro = "Tendencias";
            }

            string url =
                $"http://10.14.255.42:8001/explorar/prompts?query={Uri.EscapeDataString(query)}" +
                $"&idUsuarioActual={idUsuarioActual}" +
                $"&filtro={Uri.EscapeDataString(filtro)}";

            var responsePrompts = await _httpClient.GetAsync(url);

            if (!responsePrompts.IsSuccessStatusCode)
            {
                return new List<PromptViewModel>();
            }

            var jsonPrompts = await responsePrompts.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var prompts = JsonSerializer.Deserialize<List<PromptViewModel>>(jsonPrompts, opciones);

            if (prompts == null)
            {
                return new List<PromptViewModel>();
            }

            return prompts;
        }

        public async Task<LikeResponseViewModel?> ToggleLikeAsync(int idPrompt, int idUsuario)
        {
            string url = $"http://10.14.255.42:8001/explorar/prompts/{idPrompt}/like";

            var body = new
            {
                idUsuario = idUsuario
            };

            var json = JsonSerializer.Serialize(body);

            var responseLike = await _httpClient.PutAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (!responseLike.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonLike = await responseLike.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var likeData = JsonSerializer.Deserialize<LikeResponseViewModel>(jsonLike, opciones);

            if (likeData == null)
            {
                return null;
            }

            return likeData;
        }

        public async Task<GuardarResponseViewModel?> ToggleGuardarAsync(int idPrompt, int idUsuario)
        {
            string url = $"http://10.14.255.42:8001/explorar/prompts/{idPrompt}/guardar";

            var body = new
            {
                idUsuario = idUsuario
            };

            var json = JsonSerializer.Serialize(body);

            var responseGuardar = await _httpClient.PutAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (!responseGuardar.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonGuardar = await responseGuardar.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var guardarData = JsonSerializer.Deserialize<GuardarResponseViewModel>(jsonGuardar, opciones);

            if (guardarData == null)
            {
                return null;
            }

            return guardarData;
        }

        public async Task<ComentarioResponseViewModel?> PublicarComentarioAsync(int idPrompt, int idUsuario, string texto)
        {
            if (texto == null || texto == "")
            {
                return null;
            }

            texto = texto.Trim();

            if (texto == "")
            {
                return null;
            }

            string url = $"http://10.14.255.42:8001/explorar/prompts/{idPrompt}/comentarios";

            var body = new
            {
                idUsuario = idUsuario,
                texto = texto
            };

            var json = JsonSerializer.Serialize(body);

            var responseComentario = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var jsonComentario = await responseComentario.Content.ReadAsStringAsync();

            // Si la API rechaza el comentario, regresamos null.
            // Esto es lo que usará el controller para mostrar el warning.
            if (!responseComentario.IsSuccessStatusCode)
            {
                return null;
            }

            // Por si tu API regresa 200 pero con mensaje de error,
            // también lo detectamos aquí.
            if (jsonComentario.Contains("mala palabra", StringComparison.OrdinalIgnoreCase) ||
                jsonComentario.Contains("palabra prohibida", StringComparison.OrdinalIgnoreCase) ||
                jsonComentario.Contains("palabra no permitida", StringComparison.OrdinalIgnoreCase) ||
                jsonComentario.Contains("\"ok\":false", StringComparison.OrdinalIgnoreCase) ||
                jsonComentario.Contains("\"success\":false", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var comentarioData = JsonSerializer.Deserialize<ComentarioResponseViewModel>(jsonComentario, opciones);

            if (comentarioData == null)
            {
                return null;
            }

            return comentarioData;
        }
    }
}