using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.Widgets.Hotel
{
    public class NewDonat
    {
        public string NameDonator { get; set; }
        public decimal AmountDonat { get; set; }
        public string MessageDonat { get; set; }
        public bool Run { get; set; }
        public string Layout { get; set; }
        public DateTime DateMessage { get; set; }
        public bool HasVoice { get; set; }
        public string VoiceName { get; set; }
    }
}
