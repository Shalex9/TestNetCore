using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.API.PrivatBank
{
    public class CoursePB
    {
        public string Currency { get; set; }
        public string BaseCurrency { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
    }
}
