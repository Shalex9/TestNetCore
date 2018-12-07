using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Data;
using TestNetCore.Models;
using TestNetCore.Models.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;
using TestNetCore.Data.TableData;

namespace TestNetCore.Controllers
{
    public class APIController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private IMemoryCache cache;

        public APIController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment,
            IMemoryCache memoryCache)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
            cache = memoryCache;
        } 


        [HttpGet]
        public IActionResult PrivatBank()
        {
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

            return View(viewModel);
        }

        // Получение данных об ОФИСАХ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetOfficePB(string city)
        {
            try
            {
                List <OfficePB> office = new List<OfficePB>();
                var linkPB = "https://api.privatbank.ua/p24api/pboffice?json&city="+city+"&address=";
                var myRequest = new WebClient().DownloadString(linkPB);
                dynamic jsonFromPB = JsonConvert.DeserializeObject(myRequest);

                for (var i = 0; i < jsonFromPB.Count; i++)
                {
                    OfficePB w = new OfficePB();
                    w.Id= jsonFromPB[i].id;
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
                var linkPB = "https://api.privatbank.ua/p24api/infrastructure?json&atm&address=&city="+city;
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




        // AccuWheather
        [HttpGet]
        public IActionResult AccuWheather()
        {
            var viewModel = new AccuWheatherViewModel();
            viewModel.City = "Kyiv";
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return View(viewModel);
        }


        // Получение данных об 5 DAYS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetForecast5days(string city)
        {
            try
            {                
                var linkLocation = "http://dataservice.accuweather.com/locations/v1/cities/search?apikey=%09VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&q=" + city;
                var myRequestLocation = new WebClient().DownloadString(linkLocation);
                dynamic jsonLocation = JsonConvert.DeserializeObject(myRequestLocation);

                string cityKey = jsonLocation[0].Key;

                var link5days = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/" + cityKey + "?apikey=VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&language=ru-RU";
                var myRequest5days = new WebClient().DownloadString(link5days);
                dynamic json5days = JsonConvert.DeserializeObject(myRequest5days);
                
                List<Weather5days> list5Days = new List<Weather5days>();
                for (var i = 0; i < json5days.DailyForecasts.Count; i++)
                {
                    Weather5days w = new Weather5days();
                    w.IconPhraseDay = json5days.DailyForecasts[i].Day.IconPhrase;
                    w.IconPhraseNight = json5days.DailyForecasts[i].Night.IconPhrase;
                    w.Date = json5days.DailyForecasts[i].Date;
                    w.DateString = w.Date.ToLongDateString();
                    w.DayName = w.Date.ToString("dddd");
                    w.TemperatureMinValue = json5days.DailyForecasts[i].Temperature.Minimum.Value;
                    w.TemperatureMinUnit = json5days.DailyForecasts[i].Temperature.Minimum.Unit;
                    w.TemperatureMaxValue = json5days.DailyForecasts[i].Temperature.Maximum.Value;
                    w.TemperatureMaxUnit = json5days.DailyForecasts[i].Temperature.Maximum.Unit;
                    list5Days.Add(w);
                }

                return Json(list5Days);
            }
            catch (Exception)
            {
                string text = "Неккоректно введен город или по введенному городу данных нет.";
                return Json(text);
            }
        }


        // Получение данных об 12 HOURS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetForecast12hours(string city)
        {
            try
            {
                var linkLocation = "http://dataservice.accuweather.com/locations/v1/cities/search?apikey=%09VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&q=" + city;
                var myRequestLocation = new WebClient().DownloadString(linkLocation);
                dynamic jsonLocation = JsonConvert.DeserializeObject(myRequestLocation);

                string cityKey = jsonLocation[0].Key;

                var link12hours = "http://dataservice.accuweather.com/forecasts/v1/hourly/12hour/" + cityKey + "?apikey=VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&language=ru-RU";
                var myRequest12hours = new WebClient().DownloadString(link12hours);
                dynamic json12hours = JsonConvert.DeserializeObject(myRequest12hours);

                List<Weather12hours> list12hours = new List<Weather12hours>();
                for (var i = 0; i < json12hours.Count; i++)
                {
                    Weather12hours w = new Weather12hours();
                    w.IconPhrase = json12hours[i].IconPhrase;
                    w.TemperatureValue = json12hours[i].Temperature.Value;
                    w.TemperatureUnit = json12hours[i].Temperature.Unit;
                    w.PrecipitationProbability = json12hours[i].PrecipitationProbability;
                    w.Date = json12hours[i].DateTime;
                    w.DateString = w.Date.ToLongDateString();
                    w.TimeString = w.Date.ToString("HH:mm");
                    w.DayName = w.Date.ToString("dddd");
                    list12hours.Add(w);
                }

                return Json(list12hours);
            }
            catch (Exception)
            {
                string text = "Неккоректно введен город или по введенному городу данных нет.";
                return Json(text);
            }
        }



        // TheMovieDB
        [HttpGet]
        public IActionResult TheMovieDB()
        {
            var viewModel = new TheMovieDBViewModel();
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return View(viewModel);
        }

    }

}
