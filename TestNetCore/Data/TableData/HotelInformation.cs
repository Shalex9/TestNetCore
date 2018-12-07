using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("HotelInformations")]
    public class HotelInformation
    {
        [Key()]
        public int Id { get; set; }
        public int NumberOfRoom { get; set; }
        public int PriceForRoom { get; set; }
        public string ComfortableOfRoom { get; set; }
        public bool HasToilet { get; set; }
        public bool HasTV { get; set; }
        public bool HasBigBed { get; set; }
        public bool IsFreeNow { get; set; }
    }
}
