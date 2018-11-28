using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.WidgetsViewModels
{
    public class WidgetsBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        // Class NewDonat.cs
        public string NameDonator { get; set; }
        public decimal AmountDonat { get; set; }
        public string MessageDonat { get; set; }
        public DateTime DateMessage { get; set; }

        public string PostGenerateLink { get; set; }
        public string PostSaveSettings { get; set; }
    }

}
