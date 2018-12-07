using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("ForbiddenWords")]
    public class ForbiddenWord
    {
        [Key()]
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
