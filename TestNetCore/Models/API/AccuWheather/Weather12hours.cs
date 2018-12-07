using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.API.AccuWheather
{
    public class Weather12hours
    {
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string DayName { get; set; }
        public string IconPhrase { get; set; }
        public string TemperatureValue { get; set; }
        public string TemperatureUnit { get; set; }
        public int PrecipitationProbability { get; set; }
    }
}
