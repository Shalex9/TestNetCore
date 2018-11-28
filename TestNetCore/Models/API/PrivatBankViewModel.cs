using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.API
{
    public class PrivatBankViewModel : BaseViewModel
    {
        public string UserEmail { get; set; }

        public List<CoursePB> ListCoursePB { get; set; }        
        public PrivatBankViewModel()
        {
            ListCoursePB = new List<CoursePB>();
        }
    }
}
