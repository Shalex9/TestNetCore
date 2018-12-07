using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("ForbiddenWordUsers")]
    public class ForbiddenWordUser
    {
        [Key()]
        public int Id { get; set; }
        public string Word { get; set; }
        public string UserId { get; set; }
    }
}
