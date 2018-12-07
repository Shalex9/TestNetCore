using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestNetCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;
using TestNetCore.Models.Files.Crud;
using System.IO;
using TestNetCore.Data.TableData;

namespace TestNetCore.Controllers.Files
{
    public class CrudController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;

        public CrudController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
        }


        [HttpGet]
        [Route("crud")]
        public IActionResult Crud()
        {
            var viewModel = new CrudViewModel();

            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return View("~/Views/Files/Crud.cshtml", viewModel);
        }


        public IActionResult GetResume()
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/CV - Alexander Shevchuk.pdf");
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return PhysicalFile(file_path, file_type, file_name);
        }


        public FileResult GetStream()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/CV - Alexander Shevchuk.pdf");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return File(fs, file_type, file_name);
        }

    }
}
