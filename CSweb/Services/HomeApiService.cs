using System.Text.Json;
using CSweb.Models;

namespace CSweb.Services
{
    public class HomeApiService : IHomeApiService
    {
        private readonly HttpClient _httpClient;

        public HomeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> ObtenerUsuariosActivos()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/usuarios-activos";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return 0;

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("TotalUsuariosActivos").GetInt32();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<List<HomeTendenciaViewModel>> ObtenerTendencias()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/tendencias-actuales";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<HomeTendenciaViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var lista = new List<HomeTendenciaViewModel>();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    lista.Add(new HomeTendenciaViewModel
                    {
                        Nombre = item.GetProperty("Nombre").GetString() ?? "",
                        Posts  = item.GetProperty("TotalPromptsTendencia").GetInt32()
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<HomeTendenciaViewModel>();
            }
        }
        public async Task<List<HomeCreadorViewModel>> ObtenerUsuariosSugeridos()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/usuarios-sugeridos";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<HomeCreadorViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var lista = new List<HomeCreadorViewModel>();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    var foto = item.GetProperty("RutaFotoPerfil").GetString() ?? "";

                    lista.Add(new HomeCreadorViewModel
                    {
                        Nombre     = item.GetProperty("NombreUsuario").GetString() ?? "",
                        Categoria  = "@" + (item.GetProperty("UserName").GetString() ?? ""),
                        Imagen     = string.IsNullOrWhiteSpace(foto)
                                        ? "/Imagenes/fotosperfil/trabajador.jpg"
                                        : foto,
                        Seguidores = item.GetProperty("TotalPrompts").GetInt32().ToString()
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<HomeCreadorViewModel>();
            }
        }

        public async Task<List<HomeActividadViewModel>> ObtenerActividadesRecientes()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/actividades-recientes";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<HomeActividadViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var lista = new List<HomeActividadViewModel>();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    var fotoPerfil    = item.TryGetProperty("RutaFotoPerfil", out var fp)
                                            ? fp.GetString() ?? "" : "";
                    var fotoCategoria = item.TryGetProperty("RutaFotoCategoria", out var fc)
                                            ? fc.GetString() ?? "" : "";

                    lista.Add(new HomeActividadViewModel
                    {
                        Nombre        = item.GetProperty("NombreUsuario").GetString() ?? "",
                        Accion        = "publicó \"" + (item.GetProperty("Titulo").GetString() ?? "") + "\"",
                        Tiempo        = item.GetProperty("FechaPublicacion").GetString() ?? "",
                        Likes         = 0,
                        Comentarios   = 0,
                        ImagenPerfil  = string.IsNullOrWhiteSpace(fotoPerfil)
                                            ? "/Imagenes/fotosperfil/trabajador.jpg"
                                            : fotoPerfil,
                        ImagenPreview = string.IsNullOrWhiteSpace(fotoCategoria)
                                            ? "/Imagenes/fotosperfil/trabajador.jpg"
                                            : fotoCategoria
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<HomeActividadViewModel>();
            }
        }
        public async Task<int> ObtenerPromptsHoy()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/prompts-hoy";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return 0;

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("TotalPromptsHoy").GetInt32();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> ObtenerPromptsTotales()
        {
            try
            {
                var url = "http://127.0.0.1:5001/home/prompts-totales";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return 0;

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("TotalPrompts").GetInt32();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}