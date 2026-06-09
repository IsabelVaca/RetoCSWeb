//logica de conexion con la API
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
            // valida que query no venga null
            if (query == null)
            {
                query = "";
            }

            // valida que filtro no venga null o vacío
            if (filtro == null || filtro == "")
            {
                filtro = "Tendencias";
            }

            // arma la ruta del endpoint de explorar con query, usuario actual y filtro
            string url =
                $"http://10.14.255.42:8001/explorar/prompts?query={Uri.EscapeDataString(query)}" +
                $"&idUsuarioActual={idUsuarioActual}" +
                $"&filtro={Uri.EscapeDataString(filtro)}";

            // llama al endpoint del api para obtener los prompts
            var responsePrompts = await _httpClient.GetAsync(url);

            // valida que la respuesta del api sea correcta
            if (!responsePrompts.IsSuccessStatusCode)
            {
                return new List<PromptViewModel>();
            }

            // convierte respuesta de http a json
            var jsonPrompts = await responsePrompts.Content.ReadAsStringAsync();

            // opciones para que no importe si el json viene con mayúsculas o minúsculas
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // convierte el json en lista de prompts
            var prompts = JsonSerializer.Deserialize<List<PromptViewModel>>(jsonPrompts, opciones);
            // valida que la lista no venga null
            if (prompts == null)
            {
                return new List<PromptViewModel>();
            }
            // regresa la lista de prompts
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
            if (!responseComentario.IsSuccessStatusCode)
            {
                return null;
            }
            var jsonComentario = await responseComentario.Content.ReadAsStringAsync();
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