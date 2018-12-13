using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestNetCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Newtonsoft.Json;
using TestNetCore.Data.TableData;
using TestNetCore.Models.Widgets.Hotel;

namespace TestNetCore.Controllers
{
    public class HotelController : BaseController
    {
        public HotelController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        {
        }

        [HttpGet]
        [Route("hotel")]
        public IActionResult Hotel()
        {
            var viewModel = new HotelViewModel();
            viewModel = CreateModelForHotel(viewModel);
            viewModel.RoomReserved = false;

            return View("~/Views/Widgets/Hotel.cshtml", viewModel);
        }

        public HotelViewModel CreateModelForHotel(HotelViewModel viewModel)
        {
            DateTime now = DateTime.Now;

            CheckIsFreeRoomNow(viewModel);


            viewModel.ListRooms = _dbContext.HotelInformations.ToList();
            viewModel.ListAllReserved = _dbContext.HotelReservations.Where(u => u.EndReserv > now).ToList();
            viewModel.ListAllArhiveReserved = _dbContext.HotelReservations.Where(u => u.EndReserv < now).ToList();
            viewModel.ListUserReserved = _dbContext.HotelReservations.Where(u => u.GuestGuid == UserID && u.EndReserv > now).ToList();
            viewModel.ListUserArhiveReserved = _dbContext.HotelReservations.Where(u => u.GuestGuid == UserID && u.EndReserv < now).ToList();
            viewModel.List101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            viewModel.List102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            viewModel.List201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            viewModel.List202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            if (viewModel.ListUserReserved.Count() != 0)
                viewModel.HasUserReserve = true;
            else
                viewModel.HasUserReserve = false;

            if (viewModel.ListUserArhiveReserved.Count() != 0)
                viewModel.HasUserArhive = true;
            else
                viewModel.HasUserArhive = false;

            if (viewModel.ListAllArhiveReserved.Count() != 0)
                viewModel.HasArhive = true;
            else
                viewModel.HasArhive = false;


            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return viewModel;
        }

        public HotelViewModel CheckIsFreeRoomNow(HotelViewModel viewModel)
        {
            DateTime now = DateTime.Now;
            viewModel.ListRooms = _dbContext.HotelInformations.ToList();

            var list101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            var list102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            var list201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            var list202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            var room101 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 101);
            var room102 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 102);
            var room201 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 201);
            var room202 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 202);
            
            foreach (var i in list101Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    room101.IsFreeNow = false;
                    break;
                }
                else
                {
                    room101.IsFreeNow = true;
                }
            }

            foreach (var i in list102Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    room102.IsFreeNow = false;
                    break;
                }
                else
                {
                    room102.IsFreeNow = true;
                }
            }

            foreach (var i in list201Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    room201.IsFreeNow = false;
                    break;
                }
                else
                {
                    room201.IsFreeNow = true;
                }
            }

            foreach (var i in list202Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    room202.IsFreeNow = false;
                    break;
                }
                else
                {
                    room202.IsFreeNow = true;
                }
            }


            _dbContext.HotelInformations.Update(room101);
            _dbContext.HotelInformations.Update(room102);
            _dbContext.HotelInformations.Update(room201);
            _dbContext.HotelInformations.Update(room202);

            _dbContext.SaveChanges();

            return viewModel;
        }

        [HttpPost]
        [Route("hotel")]
        public IActionResult Hotel(HotelViewModel viewModel)
        {
            if (viewModel.PostType == "addReserve")
            {
                var room = viewModel.NumberOfRoom;
                var roomComfort = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == room).ComfortableOfRoom;
                var price = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == room).PriceForRoom;
                var countDay = (viewModel.EndReserv - viewModel.StartReserv).Days + 1;
                var summ = countDay * price;
                viewModel.SummReserv = summ;

                var reserve = new HotelReservation();

                reserve.NumberOfRoom = viewModel.NumberOfRoom;
                reserve.StartReserv = viewModel.StartReserv;
                reserve.EndReserv = viewModel.EndReserv;
                reserve.DateReserv = DateTime.Now;
                reserve.GuestGuid = UserID;
                reserve.GuestEmail = UserEmail;
                reserve.SummReserv = summ;
                reserve.GuestName = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.UserEmail == UserEmail.ToString()).UserName;

                _dbContext.HotelReservations.Add(reserve);
                
                viewModel.ListAllReserved.Add(reserve);
                viewModel.ListUserReserved.Add(reserve);
                viewModel.RoomReserved = true;
                viewModel.AlertType = "alertAddReserve";

                string textToEmail = "Уважаемый " + reserve.GuestName + "! Вы успешно забронировали номер! " +
                    "Информация о Вашей брони: Номер комнаты: " + room + ". Тип номера: " + roomComfort + ". " +
                    "Дата заезда: " + viewModel.StartReserv + ". Дата отъезда: " + viewModel.EndReserv +
                    ". Всего по оплате: " + summ + "грн. " +
                    "Ждем Вас в нашем отеле и желаем счастливого отдыха!";

                // Send Mail
                SendEMail(UserEmail, "Бронирование номера", textToEmail, "party.gif");
            }

            if (viewModel.PostType == "delReserve")
            {
                var reserveForDelete = _dbContext.HotelReservations.FirstOrDefault(u => u.Id == viewModel.IdForDelete);
                if (reserveForDelete != null)
                {
                    _dbContext.HotelReservations.Remove(reserveForDelete);
                    viewModel.AlertType = "alertDelReserve";
                    string guestName = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.UserEmail == UserEmail.ToString()).UserName;
                    var dateStart = reserveForDelete.StartReserv;
                    var dateEnd = reserveForDelete.EndReserv;
                    var roomNumber = reserveForDelete.NumberOfRoom;
                    var roomComfort = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == roomNumber).ComfortableOfRoom;

                    string textToEmail = "Уважаемый " + guestName + "! Уведомляем, что Ваша бронь отеля отменена! " +
                        "Информация о Вашей отмененной брони: Номер комнаты: " + roomNumber + ". Тип номера: " + roomComfort + ". " +
                        "Дата заезда: " + dateStart + ". Дата отъезда: " + dateEnd +
                        ". Надеемся, что еще увидимся с Вами! Всегда ждем Вас в нашем отеле и желаем счастливого отдыха!";

                    // Send Mail
                    SendEMail(UserEmail, "Отмена бронирования номера", textToEmail, "spongebob.gif");
                }
            }

            _dbContext.SaveChanges();

            viewModel = CreateModelForHotel(viewModel);

            return View("~/Views/Widgets/Hotel.cshtml", viewModel);
        }

        // Send Email  
        public static void SendEMail(string mailto, string caption, string message, string attachFile)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("alexdotnetapp@gmail.com", "Site: testdotnetapp.pp.ua");
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment("wwwroot/gallery/galleryImg/" + attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("alexdotnetapp@gmail.com", "qwerty12345678!");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }


        //AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIsFreeRoom(DateTime start, DateTime end)
        {
            DateTime now = DateTime.Now;
            DateTime today = now.AddDays(-1);
            var list101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            var list102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            var list201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            var list202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            var free = new FreeRoom();
            free.StartReserv = start;
            free.EndReserv = end;
            free.CountDays = (end - start).Days + 1;

            if (start >= today && end > now && start < end)
            {
                free.IsCorrectDate = true;

                bool free101 = true;
                foreach (var i in list101Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free101 = true;
                    }
                    else
                    {
                        free101 = false;
                        break;
                    }
                }
                if (free101)
                {
                    free.ListFreeRooms.Add(101);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 101));
                }

                bool free102 = true;
                foreach (var i in list102Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free102 = true;
                    }
                    else
                    {
                        free102 = false;
                        break;
                    }
                }
                if (free102)
                {
                    free.ListFreeRooms.Add(102);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 102));
                }

                bool free201 = true;
                foreach (var i in list201Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free201 = true;
                    }
                    else
                    {
                        free201 = false;
                        break;
                    }
                }
                if (free201)
                {
                    free.ListFreeRooms.Add(201);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 201));
                }

                bool free202 = true;
                foreach (var i in list202Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free202 = true;
                    }
                    else
                    {
                        free202 = false;
                        break;
                    }
                }
                if (free202)
                {
                    free.ListFreeRooms.Add(202);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 202));
                }


                if (free.ListFreeRooms == null)
                {
                    free.RoomEngaged = true;
                }

            }
            else
            {
                free.IsCorrectDate = false;
                free.ErrorText = "Не корректные даты бронирования";
            }

            return Json(free);
        }        
    }
}
