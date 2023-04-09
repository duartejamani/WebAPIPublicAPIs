using XcaretTestWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using XcaretTestWebApi.Servicios;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;

using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using NLog.Extensions.Logging;

namespace XcaretTestWebApi.Controllers
{
    [ApiController]
    [Route("Entradas")]
    public class Entradas : ControllerBase
    {

        //private static string _baseURL;

        private readonly IServicio_API _servicio_api;
        static HttpClient client = new HttpClient();
        private readonly ILogger<Entradas> logger;


        public Entradas(IServicio_API servicio_api, ILogger<Entradas> logger)
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //_baseURL = builder.GetSection("ApiSettings:baseURL").Value;
            _servicio_api = servicio_api;
            this.logger = logger;
            

        }

       //public List<> data = new
        [HttpGet]
        [Route("HTTPS")]
        public async Task<List<Entry>> HTTPS(bool binary)
        {
            try
            {
                logger.LogInformation("Llamando interfaz del servicio");
                var resultado = await _servicio_api.Listahttps(binary);
                logger.LogWarning($"El resultado cuenta con {resultado.Count()} datos");
                return resultado;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error al responder desde el controlador, mensaje del error : {ex.Message}");
                logger.LogError($"Error al responder desde el controlador, error text : {ex.ToString()}");
                throw new NotImplementedException();
            }


        }

        [HttpGet]
        [Route("Category")]
        public async Task<List<string>> Category()
        {
            try
            {
                logger.LogInformation("Llamando interfaz del servicio Category");
                var resultado = await _servicio_api.ListaCategory();
                return resultado;
            }
            catch(Exception ex) {
                logger.LogInformation($"Error al responder desde el controlador, mensaje del error : { ex.Message}");
                logger.LogInformation($"Error al responder desde el controlador, error text :  {ex.ToString()}");
                throw new NotImplementedException();
            }
           
            
            
        }
    }
}
