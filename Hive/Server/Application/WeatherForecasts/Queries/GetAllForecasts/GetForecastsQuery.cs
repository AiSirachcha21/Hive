using Hive.Server.Infrastructure;
using Hive.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.WeatherForecasts.Queries.GetAllForecasts
{
    public record GetForecastsQuery : IRequest<List<WeatherForecast>>;

    public class GetForecatsQueryHandler : IRequestHandler<GetForecastsQuery, List<WeatherForecast>>
    {
        private readonly ApplicationDbContext _context;

        public GetForecatsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> Handle(GetForecastsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Forecasts.ToListAsync();
        }
    }
}
