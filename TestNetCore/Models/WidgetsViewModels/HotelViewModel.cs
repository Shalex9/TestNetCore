using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.WidgetsViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        public string NameOffice { get; set; }
        public int NumberOfRoom { get; set; }
        public int PriceForRoom { get; set; }
        public string ComfortableOfRoom { get; set; }
        public bool HasToilet { get; set; }
        public bool HasTV { get; set; }
        public bool HasBigBed { get; set; }
        public bool IsFreeNow { get; set; }

        public DateTime StartReserv { get; set; }
        public DateTime EndReserv { get; set; }
        public string GuestGuid { get; set; }
        public DateTime DateReserv { get; set; }
        public int CountDays { get; set; }
        public int SummReserv { get; set; }

        public bool RoomReserved { get; set; }
        public bool RoomEngaged { get; set; }
        public bool DeletedReserved { get; set; }
        public bool HasUserReserve { get; set; }
        public bool HasUserArhive { get; set; }
        public bool HasArhive { get; set; }

        public List<HotelInformation> ListRooms { get; set; }
        public List<HotelInformation> ListFreeRooms { get; set; }
        public List<HotelReservation> ListAllReserved { get; set; }
        public List<HotelReservation> ListAllArhiveReserved { get; set; }
        public List<HotelReservation> ListUserReserved { get; set; }
        public List<HotelReservation> ListUserArhiveReserved { get; set; }
        public List<HotelReservation> List101Reserved { get; set; }
        public List<HotelReservation> List102Reserved { get; set; }
        public List<HotelReservation> List201Reserved { get; set; }
        public List<HotelReservation> List202Reserved { get; set; }
        public List<IsFreeRoom> ListIsFree { get; set; }
        public HotelViewModel()
        {
            ListRooms = new List<HotelInformation>();
            ListFreeRooms = new List<HotelInformation>();
            ListAllReserved = new List<HotelReservation>();
            ListAllArhiveReserved = new List<HotelReservation>();
            ListUserReserved = new List<HotelReservation>();
            ListUserArhiveReserved = new List<HotelReservation>();
            List101Reserved = new List<HotelReservation>();
            List102Reserved = new List<HotelReservation>();
            List201Reserved = new List<HotelReservation>();
            List202Reserved = new List<HotelReservation>();
            ListIsFree = new List<IsFreeRoom>();
        }
    }

}
