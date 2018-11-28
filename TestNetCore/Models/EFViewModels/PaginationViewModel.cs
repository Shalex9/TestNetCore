using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class PaginationViewModel :BaseViewModel
    {
        public IEnumerable<UserSort> UsersSort { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
