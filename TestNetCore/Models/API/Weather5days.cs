using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.API
{
    public class Weather5days
    {
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public string DayName { get; set; }
        public string IconPhraseDay { get; set; }
        public string IconPhraseNight { get; set; }
        public int TemperatureMinValue { get; set; }
        public string TemperatureMinUnit { get; set; }
        public int TemperatureMaxValue { get; set; }
        public string TemperatureMaxUnit { get; set; }
    }
}
