using System.Text;
using System.Text.Json;
using CSweb.Models;

namespace CSweb.Services
{
    public class TiendaApiService : ITiendaApiService
    {
        private readonly HttpClient _httpClient;

        public TiendaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ItemTiendaModel>> ObtenerItemsTienda(int idUsuario)
        {
            try
            {
                var url = $"http://127.0.0.1:8001/tienda/{idUsuario}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<ItemTiendaModel>();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var lista = new List<ItemTiendaModel>();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    lista.Add(new ItemTiendaModel
                    {
                        IDItem           = item.GetProperty("IDItem").GetInt32(),
                        NombreItem       = item.GetProperty("NombreItem").GetString() ?? "",
                        DescripcionItem  = item.GetProperty("DescripcionItem").GetString() ?? "",
                        Precio           = item.GetProperty("Precio").GetInt32(),
                        UrlImagenItem    = item.TryGetProperty("UrlImagenItem", out var url2)
                                              ? url2.GetString() ?? ""
                                              : "",
                        Desbloqueado     = item.GetProperty("Desbloqueado").GetInt32() == 1
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<ItemTiendaModel>();
            }
        }

        public async Task<SaldoCobaltsModel> ObtenerSaldoCobalts(int idUsuario)
        {
            try
            {
                var url = $"http://127.0.0.1:8001/tienda/cobalts/{idUsuario}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new SaldoCobaltsModel { Saldo = "0" };

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                return new SaldoCobaltsModel
                {
                    Saldo = doc.RootElement.GetProperty("Saldo").GetString() ?? "0"
                };
            }
            catch (Exception)
            {
                return new SaldoCobaltsModel { Saldo = "0" };
            }
        }

        public async Task<CompraResultadoModel> ComprarItem(CompraRequestModel request)
        {
            try
            {
                var url = "http://127.0.0.1:8001/tienda/comprar";
                var json = JsonSerializer.Serialize(new
                {
                    idUsuario = request.IdUsuario,
                    idItem    = request.IdItem
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                    return new CompraResultadoModel { Ok = false, Mensaje = "Error al conectar con el servidor." };

                var jsonResp = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(jsonResp);

                return new CompraResultadoModel
                {
                    Ok             = doc.RootElement.GetProperty("ok").GetInt32() == 1,
                    Mensaje        = doc.RootElement.GetProperty("mensaje").GetString() ?? "",
                    SaldoRestante  = doc.RootElement.TryGetProperty("saldoRestante", out var sr)
                                        ? sr.GetInt32()
                                        : null
                };
            }
            catch (Exception)
            {
                return new CompraResultadoModel { Ok = false, Mensaje = "Error inesperado al procesar la compra." };
            }
        }
    }
}