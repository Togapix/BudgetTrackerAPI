using BudgetTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerAPI.Controllers
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
        public async IAsyncEnumerable<Test> Get()
        {
            await using var db = new TestContext();
            Console.WriteLine($"DB path = {db.dbPath}");
            var results =
                from test in db.Test
                select test;

            await foreach (var test in results.AsAsyncEnumerable())
            {
                Console.WriteLine($"ID={test.testId} ; Name={test.name} ; Value={test.value} ; Type={test.type}");
                yield return test;
            }
        }
    }
}
