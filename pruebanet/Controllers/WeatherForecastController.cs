using Microsoft.AspNetCore.Mvc;
using pruebanet.Entities;
using pruebanet.Payloads;
using System.Text.Json;

namespace pruebanet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly MyDbContext myDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MyDbContext myDbContext)
        {
            _logger = logger;
            this.myDbContext = myDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("create")]
        public async Task<ActionResult> CrearNuevoTipoCampbio([FromBody] TipoCambio tipoCambio)
        {
            var existeTipoCambio = myDbContext.TipoCambio.Where(x => x.Fecha == tipoCambio.Fecha).FirstOrDefault();

            if (existeTipoCambio is null)
            {
                myDbContext.TipoCambio.Add(tipoCambio);
                myDbContext.SaveChanges();
            }
            else
            {
                return Ok("El tipo de cambio ya existe");
            }

            return Ok();
        }


        [HttpPost("aplicar-tipo-cambio")]
        public async Task<ActionResult> Aplicar([FromBody] ConversionPayload conversion)
        {

            var tipocambioSunat = new HttpClient();


            HttpClient client = new HttpClient();
            var resultSunat = client.GetStringAsync("https://api.apis.net.pe/v1/tipo-cambio-sunat").Result;
            var responseSunat = JsonSerializer.Deserialize<TipoCambioSunatResult>(resultSunat.ToString());


            //var existeTipoCambio = myDbContext.TipoCambio.Where(x => x.Fecha == DateTime.Now).FirstOrDefault();

            //if (existeTipoCambio is null)
            //{
            //    return Ok("Antes de convertir se necesita un tipo de cambio, por favor creelo");
            //}

            var result = new JsonResult(new
            {

                montoConTipoCambio = conversion.Monto * responseSunat.Venta,
                monedaOrigen = conversion.MondedaOrigen,
                monedaDestino = conversion.MonedaDestino,
                tipoCambio = responseSunat.Venta
            });

            return Ok(result.Value);

        }

        [HttpGet("List")]
        public async Task<ActionResult> Listar()
        {
            var result = myDbContext.TipoCambio.ToList();
            return Ok(result);
        }

    }
}