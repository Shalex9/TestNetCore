using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Models;
using TestNetCore.Models.EFViewModels;
using TestNetCore.Controllers;

namespace TestNetCore.Models.EFViewModels
{
    public class PhonesViewModel : BaseViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
        public int? ChoosenCompany { get; set; }
    }
}
