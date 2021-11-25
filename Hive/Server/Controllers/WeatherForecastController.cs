using Hive.Server.Application.WeatherForecasts.Queries.GetAllForecasts;
using Hive.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiController
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetForecastsQuery());
        }
    }
}
