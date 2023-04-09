using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System.Net.Http.Headers;
using System.Text;
using XcaretTestWebApi.Controllers;
using XcaretTestWebApi.Models;

namespace XcaretTestWebApi.Servicios
{
    public class Servicio_API : IServicio_API
    {
        

        private static string _baseURL;
        private readonly ILogger<Servicio_API> logger;

        public Servicio_API(ILogger<Servicio_API> logger)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseURL = builder.GetSection("ApiSettings:baseURL").Value;
            this.logger = logger;
        }

        public async Task<List<Entry>> Listahttps(bool binary)
        {
            List<Entry> entries = new List<Entry>();
            logger.LogInformation("Listahttps");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                        string url = _baseURL + "entries";
                        client.DefaultRequestHeaders.Clear();
                        var response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            string json_respuesta = await response.Content.ReadAsStringAsync();
                            JsonResponse listentriess = JsonConvert.DeserializeObject<JsonResponse>(json_respuesta);
                            entries = listentriess.entries;
                            var test = entries.FirstOrDefault();
                            var result = listentriess.entries.Where(s => s.HTTPS == binary).ToList();
                            return result;
                        }
                        else
                        {
                            return new List<Entry>();
                        }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error al responder desde el Servicio, mensaje del error : {ex.Message}");
                logger.LogError($"Error al responder desde el Servicio, error text : {ex.ToString()}");
                return new List<Entry>();
            }
        }

        public async Task<List<string>> ListaCategory()
        {
            logger.LogInformation("category");
            List<Entry> entries = new List<Entry>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = _baseURL + "entries";
                    client.DefaultRequestHeaders.Clear();
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string json_respuesta = await response.Content.ReadAsStringAsync();
                        JsonResponse listentriess = JsonConvert.DeserializeObject<JsonResponse>(json_respuesta);
                        entries = listentriess.entries;
                        var test = entries.FirstOrDefault();
                        var result2 = listentriess.entries.Select(a => a.Category).Distinct().ToList();
                        return result2;
                    }
                    else
                    {
                        return new List<string>();
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Error al responder desde el Servicio, mensaje del error : {ex.Message}");
                logger.LogError($"Error al responder desde el Servicio, error text : {ex.ToString()}");
                return new List<string>();
            }
           
        }
    }
}
