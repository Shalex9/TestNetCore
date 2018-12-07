using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TestNetCore.Data;
using TestNetCore.Models;
using TestNetCore.Data.TableData;

namespace TestNetCore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private IMemoryCache cache;

        public HomeController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment,
            IMemoryCache memoryCache)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
            cache = memoryCache;
        }
        public IActionResult Index()
        {
            //HomeViewModel viewModel = new HomeViewModel();
            //viewModel.UserName = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.Id == 1).UserName;
            //viewModel.UserEmail = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.Id == 1).UserEmail;

            //ViewData["UserEmail"] = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.Id == 1).UserEmail;
            ////viewModel.UserEmail = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.Id == 1).UserEmail;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
