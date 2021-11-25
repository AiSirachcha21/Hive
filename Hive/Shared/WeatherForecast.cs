using System;
using System.Collections.Generic;
using System.Text;

namespace Hive.Shared
{
    public class WeatherForecast
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
