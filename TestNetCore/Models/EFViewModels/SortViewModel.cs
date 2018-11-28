using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Controllers;

namespace TestNetCore.Models.EFViewModels
{
    public class SortViewModel : BaseViewModel
    {
        public IEnumerable<UserSort> SortList { get; set; }
        public SortUserViewModel SortUserViewModel { get; set; }
    }
}
