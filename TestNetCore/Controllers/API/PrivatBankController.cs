using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Data;
using TestNetCore.Models.API.PrivatBank;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Logging;

namespace TestNetCore.Controllers.API
{
    public class PrivatBankController : BaseController
    {
        ILoggerFactory _logger;

        public PrivatBankController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            ILoggerFactory loggerFactory)
            : base(httpContextAccessor, dbContext)
        {
            _logger = loggerFactory;
        }


        [HttpGet]
        [Route("privatbank")]
        public IActionResult PrivatBank()
        {
            _logger.CreateLogger("Hello");
            var viewModel = new PrivatBankViewModel();

            var linkCoursePB = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
            var coursePB = new WebClient().DownloadString(linkCoursePB);
            dynamic jsonCoursePB = JsonConvert.DeserializeObject(new WebClient().DownloadString(linkCoursePB));

            for (var i = 0; i < jsonCoursePB.Count; i++)
            {
                CoursePB w = new CoursePB();
                w.BaseCurrency = jsonCoursePB[i].base_ccy;
                w.Currency = jsonCoursePB[i].ccy;
                w.Buy = jsonCoursePB[i].buy;
                w.Sale = jsonCoursePB[i].sale;
                viewModel.ListCoursePB.Add(w);
            }

            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return View("~/Views/API/PrivatBank.cshtml", viewModel);
        }

        // Получение данных об ОФИСАХ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetOfficePB(string city)
        {
            try
            {
                List<OfficePB> office = new List<OfficePB>();
                var linkPB = "https://api.privatbank.ua/p24api/pboffice?json&city=" + city + "&address=";
                var myRequest = new WebClient().DownloadString(linkPB);
                dynamic jsonFromPB = JsonConvert.DeserializeObject(myRequest);

                for (var i = 0; i < jsonFromPB.Count; i++)
                {
                    OfficePB w = new OfficePB();
                    w.Id = jsonFromPB[i].id;
                    w.Country = jsonFromPB[i].country;
                    w.State = jsonFromPB[i].state;
                    w.City = jsonFromPB[i].city;
                    w.Name = jsonFromPB[i].name;
                    w.Phone = jsonFromPB[i].phone;
                    w.Address = jsonFromPB[i].address;
                    w.Email = jsonFromPB[i].email;
                    office.Add(w);
                }

                return Json(office);
            }
            catch (Exception)
            {
                string text = "Неккоректно введен город или по введенному городу данных нет.";
                return Json(text);
            }
        }


        // Получение данных об Банкоматах
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetAtmPB(string city)
        {
            try
            {
                var linkPB = "https://api.privatbank.ua/p24api/infrastructure?json&atm&address=&city=" + city;
                var myRequest = new WebClient().DownloadString(linkPB);
                dynamic jsonFromPB = JsonConvert.DeserializeObject(myRequest);

                //List<AtmPB> atm = new List<AtmPB>();
                //for (var i = 0; i < jsonFromPB.Count; i++)
                //{
                //    AtmPB w = new AtmPB();
                //    w.CityRu = jsonFromPB[i].cityRU;
                //    w.FullAdressRu = jsonFromPB[i].fullAddressRu;
                //    w.PlaceRu = jsonFromPB[i].rlaceRu;
                //    atm.Add(w);
                //}

                return Json(jsonFromPB);
            }
            catch (Exception)
            {
                string text = "Неккоректно введен город или по введенному городу данных нет.";
                return Json(text);
            }
        }

        // Получение данных об ТЕРМИНАЛАХ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetTsoPB(string city)
        {
            try
            {
                var linkPB = "https://api.privatbank.ua/p24api/infrastructure?json&tso&address=&city=" + city;
                var myRequest = new WebClient().DownloadString(linkPB);
                dynamic jsonFromPB = JsonConvert.DeserializeObject(myRequest);

                return Json(jsonFromPB);
            }
            catch (Exception)
            {
                string text = "Неккоректно введен город или по введенному городу данных нет.";
                return Json(text);
            }
        }

    }

}
