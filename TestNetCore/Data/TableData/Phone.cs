using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("Phones")]
    public class Phone
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Manufacturer { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Diagonal { get; set; }
    }
}
