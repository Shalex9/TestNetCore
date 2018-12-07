using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Widgets.Hotel
{
    public class FreeRoom
    {
        public DateTime StartReserv { get; set; }
        public DateTime EndReserv { get; set; }
        public bool RoomEngaged { get; set; }
        public int CountDays { get; set; }

        public bool IsCorrectDate { get; set; }
        public string ErrorText { get; set; }

        public List<int> ListFreeRooms { get; set; }
        public List<HotelInformation> ListInfoFreeRooms { get; set; }

        public FreeRoom()
        {
            ListFreeRooms = new List<int>();
            ListInfoFreeRooms = new List<HotelInformation>();
        }
    }
}
