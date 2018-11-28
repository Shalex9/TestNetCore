using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class UsersListViewModel : BaseViewModel
    {
        public IEnumerable<UserSort> Users { get; set; }
        public SelectList Companies { get; set; }
        public string Name { get; set; }
    }
}
