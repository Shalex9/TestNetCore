using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("CompaniesSort")]
    public class CompanySort
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserSort> UsersSort { get; set; }
        public CompanySort()
        {
            UsersSort = new List<UserSort>();
        }
    }
}
