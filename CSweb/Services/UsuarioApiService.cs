using CSweb.Models;
using System.Text.Json;

namespace CSweb.Services
{
    public class UsuarioAPIService : IUsuarioAPIService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://10.14.255.42:8001";

        public UsuarioAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool ok, int? idUsuario, string? mensaje)> LoginUsuarioAsync(
            string username,
            string contrasena)
        {
            try
            {
                var url =
                    $"{BaseUrl}/loginUsuario" +
                    $"?username={Uri.EscapeDataString(username)}" +
                    $"&contrasena={Uri.EscapeDataString(contrasena)}";

                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var jsonOpts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (!response.IsSuccessStatusCode)
                {
                    var err = JsonSerializer.Deserialize<LoginApiErrorViewModel>(json, jsonOpts);
                    var texto = err?.Mensaje ?? err?.Error ?? "Usuario o contraseña incorrectos.";
                    return (false, null, texto);
                }

                var usuario = JsonSerializer.Deserialize<UsuarioLoginViewModel>(json, jsonOpts);
                if (usuario == null || usuario.IdUsuario <= 0)
                    return (false, null, "Respuesta de login inválida.");

                return (true, usuario.IdUsuario, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en LoginUsuarioAsync:");
                Console.WriteLine(ex.Message);
                return (false, null, "No se pudo conectar con la API de login.");
            }
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