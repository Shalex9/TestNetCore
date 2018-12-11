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
using System.Text;

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
                if (getUserFiles.FileName1 != null) viewModel.FileName1 = getUserFiles.FileName1;
                if (getUserFiles.FileSize1 != null) viewModel.FileSize1 = getUserFiles.FileSize1;
                if (getUserFiles.FileName2 != null) viewModel.FileName2 = getUserFiles.FileName2;
                if (getUserFiles.FileSize2 != null) viewModel.FileSize2 = getUserFiles.FileSize2;
                viewModel.DateChange = getUserFiles.LastChange;
            }

            // смотрю, один или два файла уже имеется
            var isUserOneFile = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && ((a.FileName1 != null && a.FileName2 == null) || (a.FileName1 == null && a.FileName2 != null)) );
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
        
        public CrudViewModel UploadFile(dynamic file, CrudViewModel viewModel)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);
            var size = (file.Length / 1024) + "КБ";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            SaveFileToDB(fileName, path, size, viewModel);

            return viewModel;
        }

        public CrudViewModel SaveFileToDB(string fileName, string path, string size, CrudViewModel viewModel)
        {
            var now = DateTime.Now;
            var isAnyFile = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID && (a.FileName1 != null || a.FileName2 != null));

            if (isAnyFile == null)
            {
                viewModel.FileName1 = fileName;
                viewModel.FileSize1 = size;
                viewModel.DateChange = now;

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
                viewModel.FileName2 = fileName;
                viewModel.FileSize2 = size;
                viewModel.DateChange = now;

                isAnyFile.FileName2 = fileName;
                isAnyFile.FilePath2 = path;
                isAnyFile.FileSize2 = size;
                isAnyFile.LastChange = now;

                _dbContext.CRUDfileUsers.Update(isAnyFile);
            }
            else if (isAnyFile.FileName1 == null && isAnyFile.FileName2 != null)
            {
                viewModel.FileName1 = fileName;
                viewModel.FileSize1 = size;
                viewModel.DateChange = now;

                isAnyFile.FileName1 = fileName;
                isAnyFile.FilePath1 = path;
                isAnyFile.FileSize1 = size;
                isAnyFile.LastChange = now;

                _dbContext.CRUDfileUsers.Update(isAnyFile);
            }
            _dbContext.SaveChanges();

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
                            UploadFile(file, viewModel);
                        }
                    }
                    viewModel.VerifyChanges = true;
                    viewModel.TextAlert = "createFile";
                }
            }

            if (viewModel.PostType == "createFile" && viewModel.NameFile != "" && viewModel.NameFile != null)
            {
                string fileName = viewModel.NameFile + ".txt";
                string path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);
                string size;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(viewModel.TextFile);
                    stream.Write(info, 0, info.Length);
                    size = (info.Length / 1024) + "КБ";
                }
                
                viewModel.TextAlert = "createFile";
                SaveFileToDB(fileName, path, size, viewModel);
            }

            if (viewModel.PostType == "deleteFile" && viewModel.NameFile != "" && viewModel.NameFile != null)
            {
                string fileName = viewModel.NameFile;
                string path = "wwwroot/usersfiles/" + UserID + "/" + fileName;
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }

                var now = DateTime.Now;
                var userRecord = _dbContext.CRUDfileUsers.FirstOrDefault(a => a.UserId == UserID);
                if (userRecord.FileName1 == fileName)
                {
                    userRecord.FileName1 = null;
                    userRecord.FilePath1 = null;
                    userRecord.FileSize1 = null;
                    userRecord.LastChange = now;
                }
                else if (userRecord.FileName2 == fileName)
                {
                    userRecord.FileName2 = null;
                    userRecord.FilePath2 = null;
                    userRecord.FileSize2 = null;
                    userRecord.LastChange = now;
                }

                viewModel.TextAlert = "deleteFile";
                _dbContext.CRUDfileUsers.Update(userRecord);
                _dbContext.SaveChanges();
            }

            var vm = ModelForCrud(viewModel);

            return View("~/Views/Files/Crud.cshtml", vm);
        }


        // Чтение файла
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReadFile(string fileName)
        {
            CrudViewModel viewModel = new CrudViewModel();
            string path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    viewModel.TextFile = sr.ReadToEnd();
                }
            }

            var vm = ModelForCrud(viewModel);
            return Json(vm);
        }


        // Сохранение изменений в файле
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveChangesFile(string fileName, string fileText)
        {
            string path = Path.Combine("wwwroot/usersfiles/" + UserID + "/", fileName);
            
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(fileText);
            }

            return Json("ReWrite successed!");
        }


        // Загрузка файла
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DownloadFile(string fileName)
        {
            GetFile(fileName);
            Download(fileName);
            return Json("Download success!!!");
        }

        public VirtualFileResult Download(string fileName)
        {
            string path = $"wwwroot/usersfiles/{UserID}/{fileName}";
            var filepath = Path.Combine(_appEnvironment.ContentRootPath, path);
            return File(path, "text/plain", fileName);
        }

        // Скачивание файлов
        public IActionResult GetFile(string fileName)
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/" + fileName);
            string type = "text/plain";
            return PhysicalFile(path, type, fileName);
        }


        // Скачивание резюме
        public IActionResult GetResume()
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/CV - Alexander Shevchuk.pdf");
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return PhysicalFile(file_path, file_type, file_name);
            //string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/" + UserID + "/" + fileName);
            //string type = "text/plain";
            //return PhysicalFile(path, type, fileName);

        }

    }
}
