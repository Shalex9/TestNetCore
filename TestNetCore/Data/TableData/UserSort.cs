using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNetCore.Data.TableData
{
    [Table("UsersSort")]
    public class UserSort
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CompanyId { get; set; }
        public CompanySort Company { get; set; }
    }
}
