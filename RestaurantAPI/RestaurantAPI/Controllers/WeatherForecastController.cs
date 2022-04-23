using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
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

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery] int results, [FromQuery] int minTemp, [FromQuery] int maxTemp)
        {
            if (results.GetType() != typeof(int) | minTemp.GetType() != typeof(int) | maxTemp.GetType() != typeof(int))
            {
                return StatusCode(400);
            }

            return StatusCode(200, Enumerable.Range(1, results).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(minTemp, maxTemp),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
       
       

    }
}