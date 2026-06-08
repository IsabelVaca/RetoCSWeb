using CSweb.Models;
using System.Text.Json;

namespace CSweb.Services
{
    public class UsuarioAPIService : IUsuarioAPIService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://127.0.0.1:8001";

        public UsuarioAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioRankingViewModel>> ObtenerUsuariosRanking()
        {
            try
            {
                string url = $"{BaseUrl}/ranking";

                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("URL ranking general: " + url);
                Console.WriteLine("STATUS ranking general: " + response.StatusCode);
                Console.WriteLine("JSON ranking general:");
                Console.WriteLine(json);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<UsuarioRankingViewModel>();
                }

                var usuarios = JsonSerializer.Deserialize<List<UsuarioRankingViewModel>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return usuarios ?? new List<UsuarioRankingViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en ObtenerUsuariosRanking:");
                Console.WriteLine(ex.Message);
                return new List<UsuarioRankingViewModel>();
            }
        }

        public async Task<List<UsuarioRankingViewModel>> ObtenerRankingLikes()
        {
            try
            {
                string url = $"{BaseUrl}/ranking/likes";

                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("URL ranking likes: " + url);
                Console.WriteLine("STATUS ranking likes: " + response.StatusCode);
                Console.WriteLine("JSON ranking likes:");
                Console.WriteLine(json);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<UsuarioRankingViewModel>();
                }

                var usuarios = JsonSerializer.Deserialize<List<UsuarioRankingViewModel>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return usuarios ?? new List<UsuarioRankingViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en ObtenerRankingLikes:");
                Console.WriteLine(ex.Message);
                return new List<UsuarioRankingViewModel>();
            }
        }

        public async Task<DetallePuntosViewModel?> ObtenerDetallePuntosUsuario(int idUsuario)
        {
            try
            {
              
                string url = $"{BaseUrl}/ranking/{idUsuario}/detalle-puntos";

                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("URL detalle puntos: " + url);
                Console.WriteLine("STATUS detalle puntos: " + response.StatusCode);
                Console.WriteLine("JSON detalle puntos:");
                Console.WriteLine(json);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var detalle = JsonSerializer.Deserialize<DetallePuntosViewModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return detalle;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en ObtenerDetallePuntosUsuario:");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}