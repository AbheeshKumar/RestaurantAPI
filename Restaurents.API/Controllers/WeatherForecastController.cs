using Microsoft.AspNetCore.Mvc;

namespace Restaurent.API.Controllers;

public class TemperatureRange
{
    public int MaxTemp { get; set; }
    public int MinTemp { get; set; }
}

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;
    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    [Route("{take}/example")]
    public IEnumerable<WeatherForecast> Get([FromQuery] int max,  [FromRoute] int take)
    {
        var result = _weatherForecastService.Get();
        return result;
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromBody] TemperatureRange temperatureRange, [FromQuery] int count)
    {
        if (temperatureRange.MinTemp > temperatureRange.MaxTemp && count < 0)
        {
            return BadRequest("Invalid Data");
        }
        var result = _weatherForecastService.Get(temperatureRange.MinTemp, temperatureRange.MaxTemp, count);
        return Ok(result);
    }

    [HttpGet("currentDay")]
    public WeatherForecast GetCurrentDay()
    {
        var result = _weatherForecastService.Get().First();
        return result;
    }

}

