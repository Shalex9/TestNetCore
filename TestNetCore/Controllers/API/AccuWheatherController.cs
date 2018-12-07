using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Data;
using TestNetCore.Models.API.AccuWheather;
using Newtonsoft.Json;
using System.Net;

namespace TestNetCore.Controllers.API
{
    public class AccuWheatherController : BaseController
    {
        public AccuWheatherController (
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        {  }


    // AccuWheather
    [HttpGet]
    [Route("accuwheather")]
    public IActionResult AccuWheather()
    {
        var viewModel = new AccuWheatherViewModel();
        viewModel.City = "Kyiv";
        viewModel.UserName = CurrentUserName;
        viewModel.AvatarPath = CurrentAvatarPath;

        return View("~/Views/API/AccuWheather.cshtml", viewModel);
    }


    // Получение данных об 5 DAYS
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetForecast5days(string city)
    {
        try
        {
            var linkLocation = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey=%09VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&q={city}";
            var myRequestLocation = new WebClient().DownloadString(linkLocation);
            dynamic jsonLocation = JsonConvert.DeserializeObject(myRequestLocation);

            string cityKey = jsonLocation[0].Key;

            var link5days = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{cityKey}?apikey=VG1UYOrge3fvRj4zD1NAUnyaSzYgK9Oy&language=ru-RU";
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

}
}
