using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class AddUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public int UserCompanyId { get; set; }
        public CompanySort Company { get; set; }
    }
}
