using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class SortData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CompanyId { get; set; }
        public CompanySort Company { get; set; }
    }
}
