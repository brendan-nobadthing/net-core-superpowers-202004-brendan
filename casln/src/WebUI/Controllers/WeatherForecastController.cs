using System;
using casln.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace casln.WebUI.Controllers
{
    public class WeatherForecastController : ApiController
    {

        private readonly ILogger<WeatherForecastController> _log;

        public WeatherForecastController(ILogger<WeatherForecastController> log)
        {
            _log = log;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _log.LogInformation("Get Weather Forecast");
            _log.LogInformation("Get weather forecast called at {DATE} ", DateTime.Now.ToShortDateString());

            var result  = await Mediator.Send(new GetWeatherForecastsQuery());
            _log.LogInformation("Get weather forecast retuning {@Result} ", result);

            return result;
        }
    }
}
