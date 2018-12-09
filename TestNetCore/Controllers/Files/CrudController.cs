﻿using System;
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
            viewModel = ModelForCrud(viewModel);

            return View("~/Views/Files/Crud.cshtml", viewModel);
        }

        public CrudViewModel ModelForCrud(CrudViewModel viewModel)
        {
            // Проверяю или существует директория для файлов для этого юзера, если нет - создаю
            string directoryFilesUser = "wwwroot/usersfiles/" + UserID + "/";
            if (!Directory.Exists(directoryFilesUser))
            {
                Directory.CreateDirectory(directoryFilesUser);
            }

            // Есть ли в БД запись с файлами для этого юзера
            var getUserFiles = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID);
            if (getUserFiles != null)
            {
                viewModel.FileName1 = getUserFiles.FileName1;
                viewModel.FileSize1 = getUserFiles.FileSize1;
                viewModel.DateChange = getUserFiles.LastChange;
                if (getUserFiles.FileName2 != null) viewModel.FileName2 = getUserFiles.FileName2;
                if (getUserFiles.FileSize2 != null) viewModel.FileSize2 = getUserFiles.FileSize2;
            }

            // смотрю, один или два файла уже имеется
            var isUserOneFile = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && a.FileName1 != null && a.FileName2 == null);
            var isUserTwoFiles = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && a.FileName1 != null && a.FileName2 != null);
            if (isUserOneFile == null && isUserTwoFiles == null)
            {
                viewModel.CountFiles = 0;
            }
            else if (isUserOneFile != null && isUserTwoFiles == null)
            {
                viewModel.CountFiles = 1;
            }
            else
            {
                viewModel.CountFiles = 2;
            }

            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return viewModel;
        }

        [HttpPost]
        [Route("crud")]
        public IActionResult Crud(List<IFormFile> files, CrudViewModel viewModel)
        {           
            if (viewModel.PostType == "uploadFiles" && viewModel.CountFiles != 2)
            {
                if (viewModel.TypeUploadFile == "text")
                {
                    foreach (var file in files)
                    {
                        // загружаю сам файл в нужную дирректорию
                        if (file != null && file.Length > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);
                            var size = (file.Length / 1024) + "КБ";
                            var now = DateTime.Now;
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            // записую этот файл в БД
                            var isAnyFile = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && (a.FileName1 != null || a.FileName2 != null));

                            if (isAnyFile == null)
                            {
                                CRUDfileUser newFile = new CRUDfileUser();
                                newFile.UserId = UserID;
                                newFile.FileName1 = fileName;
                                newFile.FilePath1 = path;
                                newFile.FileSize1 = size;
                                newFile.LastChange = now;

                                _dbContext.CRUDfileUsers.Add(newFile);
                            }
                            else if (isAnyFile.FileName1 != null && isAnyFile.FileName2 == null)
                            {
                                isAnyFile.FileName2 = fileName;
                                isAnyFile.FilePath2 = path;
                                isAnyFile.FileSize2 = size;
                                isAnyFile.LastChange = now;

                                _dbContext.CRUDfileUsers.Update(isAnyFile);
                            }
                            else if (isAnyFile.FileName1 == null && isAnyFile.FileName2 != null)
                            {
                                isAnyFile.FileName1 = fileName;
                                isAnyFile.FilePath1 = path;
                                isAnyFile.FileSize1 = size;
                                isAnyFile.LastChange = now;

                                _dbContext.CRUDfileUsers.Update(isAnyFile);
                            }
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }

            var vm = ModelForCrud(viewModel);

            return View("~/Views/Files/Crud.cshtml", vm);
        }


        // Создание файла
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFile(string fileName)
        {
            CrudViewModel vm = new CrudViewModel();
            vm = ModelForCrud(vm);
            var size = "0 КБ";
            var path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);
            var now = DateTime.Now;
            var isAnyFile = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && a.FileName1 != null);

            if (vm.CountFiles == 0)
            {
                vm.FileName1 = fileName;
                vm.FileSize1 = size;
                vm.DateChange = now;
                vm.CountFiles = 1;

                CRUDfileUser newFile = new CRUDfileUser();
                newFile.UserId = UserID;
                newFile.FileName1 = fileName;
                newFile.FilePath1 = path;
                newFile.FileSize1 = size;
                newFile.LastChange = now;

                _dbContext.CRUDfileUsers.Add(newFile);
            }
            else if (vm.CountFiles == 1)
            {
                vm.FileName2 = fileName;
                vm.FileSize2 = size;
                vm.DateChange = now;
                vm.CountFiles = 2;

                isAnyFile.FileName2 = fileName;
                isAnyFile.FilePath2 = path;
                isAnyFile.FileSize2 = size;
                isAnyFile.LastChange = now;

                _dbContext.CRUDfileUsers.Update(isAnyFile);
            }
            else if (vm.CountFiles ==2)
            {

            }

            _dbContext.SaveChanges();


            return Json("Hello");
        }



        // Скачивание резюме
        public IActionResult GetResume()
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/CV - Alexander Shevchuk.pdf");
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return PhysicalFile(file_path, file_type, file_name);
        }

    }
}
