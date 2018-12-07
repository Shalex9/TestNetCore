using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("HotelReservations")]
    public class HotelReservation
    {
        [Key()]
        public int Id { get; set; }
        public int NumberOfRoom { get; set; }
        public DateTime StartReserv { get; set; }
        public DateTime EndReserv { get; set; }
        public string GuestGuid { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime DateReserv { get; set; }
        public int SummReserv { get; set; }
    }
}
