using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication1.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public  IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("HELLOOOOO");
            Task.Delay(6000);
            using SqlConnection connection = new("Data Source=SA-PC120;Initial Catalog=Test1;Integrated Security=True");
            using SqlCommand query = new("SELECT * FROM Test1.Product", connection);
            connection.Open();
            using SqlDataReader rdr = query.ExecuteReader();
            connection.Close();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //[HttpGet(Name = "Test")]
        //public string Test()
        //{
        //    using SqlConnection connection = new("Data Source=SA-PC120;Initial Catalog=Test1;Integrated Security=True");
        //    using SqlCommand query = new("SELECT * FROM Test1.User",connection);
        //    connection.Open();
        //    using SqlDataReader rdr = query.ExecuteReader();
        //    connection.Close();

        //    return "helloo";
        //}
    }
}