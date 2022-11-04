using Microsoft.AspNetCore.Mvc;

namespace AppInsightService.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("forecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Weather is good here");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 60),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            
        }

        [HttpGet("errortest")]
        public IEnumerable<WeatherForecast> GetError()
        {
            int j =  0;
            try
            {
                int k = 10 / j;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("Weather is not good here");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 60),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("faulttest")]
        public IEnumerable<WeatherForecast> GetFault()
        {
            _logger.LogInformation("Weather is terrible here");
            int j = 0;
            int k = 10 / j;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 60),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        public string GetTest()
        {
            return "success 1";
        }
    }
}