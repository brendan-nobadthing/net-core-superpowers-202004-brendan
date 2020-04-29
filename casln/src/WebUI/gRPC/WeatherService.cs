using System;
using System.Linq;
using System.Threading.Tasks;
using casln.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Weather;

namespace casln.WebUI.gRPC
{
    public class WeatherService : Weather.WeatherService.WeatherServiceBase
    {

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public WeatherService(ILogger<WeatherService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public override async Task<WeatherResponse> GetWeather(WeatherRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC Get Weather");
            var forecasts = await _mediator.Send(new GetWeatherForecastsQuery());

            var result = new WeatherResponse();
            result.Items.AddRange(forecasts.Select(f => new WeatherItem()
            {
                Date = (long)(f.Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)))
                    .TotalSeconds, // convert to unix time
                Syummary = f.Summary,
                TempC = f.TemperatureC,
                TempF = f.TemperatureF
            }));
            return result;
        }
    }
}