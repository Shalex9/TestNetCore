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
using TestNetCore.Models.Widgets.Messages;

namespace TestNetCore.Controllers
{
    public class MessagesController : BaseController
    {
        public MessagesController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        {
        }


        [HttpGet]
        [Route("messages")]
        public IActionResult Messages()
        {
            var viewModel = new MessagesViewModel();

            viewModel = ModelForMessages(viewModel);
            return View("~/Views/Widgets/Messages.cshtml", viewModel);
        }

        public MessagesViewModel ModelForMessages(MessagesViewModel viewModel)
        {
            // Проверяю или существует директория для файлов для этого юзера, если нет - создаю
            string directoryFilesUser = "wwwroot/usersfiles/" + UserID + "/imgForEmail/";
            if (!Directory.Exists(directoryFilesUser))
            {
                Directory.CreateDirectory(directoryFilesUser);
            }


            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;
            return viewModel;
        }

        [HttpPost]
        [Route("hotel")]
        public IActionResult Messages(List<IFormFile> files, MessagesViewModel viewModel)
        {

            SendEMail(UserEmail, viewModel.NameFrom, viewModel.TitleMessage, viewModel.TextMessage, viewModel.AttachFile, UserID);

            viewModel = ModelForMessages(viewModel);

            return View("~/Views/Widgets/Hotel.cshtml", viewModel);
        }

        public MessagesViewModel UploadFile(dynamic file, MessagesViewModel viewModel)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine("wwwroot/usersfiles/" + UserID + "/imgForEmail/", fileName);
            var size = (file.Length / 1024) + "КБ";

            if (size <= 15000)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return viewModel;
        }


        // Send Email  
        public static void SendEMail(string mailto, string name, string title, string message, string attachFile, string id)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("alexdotnetapp@gmail.com", name + "(, from test site: testdotnetapp.pp.ua)");
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = title;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment("wwwroot/usersfiles/" + id + "/imgForEmail/" + attachFile));
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

    }
}