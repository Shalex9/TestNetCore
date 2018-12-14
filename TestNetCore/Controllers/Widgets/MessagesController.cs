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
using System.Text.RegularExpressions;

namespace TestNetCore.Controllers
{
    public class MessagesController : BaseController
    {
        public MessagesController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        { }


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

            viewModel.SendTo = UserEmail;
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;
            return viewModel;
        }


        [HttpPost]
        [Route("messages")]
        public IActionResult Messages(List<IFormFile> files, MessagesViewModel viewModel)
        {
            if (viewModel.PostType == "uploadFiles")
            {
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        viewModel.AttachFile = UploadFile(file);
                    }
                }
            }

            if (viewModel.PostType == "sendEmail")
            {
                // Create List Forbidden Words (Standart Forbidden Words + User )
                var forbiddenWordsUser = _dbContext.ForbiddenWordUsers.Where(u => u.UserId == UserID);
                var forbiddenWordStandart = _dbContext.ForbiddenWords.Where(a => a.Word != null);

                // for Change forbidden words to ***
                if (viewModel.TextMessage != null)
                {
                    viewModel.TextMessage = RemoveWords(viewModel.TextMessage, forbiddenWordsUser, forbiddenWordStandart, viewModel);
                }
                SendEMail(UserEmail, viewModel.NameFrom, viewModel.TitleMessage, viewModel.TextMessage, viewModel.AttachFile, UserID);
            }

            if (viewModel.PostType == "saveSettings")
            {
                var forbiddenWord = _dbContext.ForbiddenWordUsers.Where(u => u.UserId == UserID);
                var forbiddenWordStandart = _dbContext.ForbiddenWords.Where(a => a.Word != null);

                var stringForbiddenWords = viewModel.StringForbiddenWordsUser;
                if (stringForbiddenWords != null)
                {
                    var listForbiddenWords = stringForbiddenWords.Trim().Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

                    // удаляю из БД все запрещенные слова юзера
                    var wordsUser = _dbContext.ForbiddenWordUsers.Where(a => a.UserId == UserID);
                    foreach (var del in wordsUser)
                    {
                        _dbContext.ForbiddenWordUsers.Remove(del);
                    }
                    _dbContext.SaveChanges();

                    if (listForbiddenWords != null)
                    {
                        foreach (string word in listForbiddenWords)
                        {
                            ForbiddenWordUser w = new ForbiddenWordUser();
                            // на всякий случай сравниваю массив новых слов юзера со словами из таблицы, если такого слова нет - добавляю в таблицу
                            if (_dbContext.ForbiddenWordUsers.Where(a => a.Word == word).Count() == 0)
                            {
                                w.UserId = UserID;
                                w.Word = word.ToLower();
                                viewModel.ForbiddenWordsUser.Add(w);
                                _dbContext.ForbiddenWordUsers.Add(w);
                            }
                        }
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    // просто удаляю из БД все запрещенные слова юзера
                    var wordsUser = _dbContext.ForbiddenWordUsers.Where(a => a.UserId == UserID);
                    foreach (var del in wordsUser)
                    {
                        _dbContext.ForbiddenWordUsers.Remove(del);
                    }
                    _dbContext.SaveChanges();
                }
            }

            viewModel = ModelForMessages(viewModel);

            return View("~/Views/Widgets/Messages.cshtml", viewModel);
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


        public string UploadFile(dynamic file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine("wwwroot/usersfiles/" + UserID + "/imgForEmail/", fileName);
            var size = (file.Length / 1024) + "КБ";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }


        public static string RemoveWords(string textForReplace,
                                    IQueryable<ForbiddenWordUser> forbiddenWordUser,
                                    IQueryable<ForbiddenWord> forbiddenWordStandart,
                                    MessagesViewModel viewModel)
        {
            foreach (var word in forbiddenWordUser)
            {
                var forbidden = new ForbiddenWordUser();
                forbidden.Word = word.Word;
                viewModel.ForbiddenWordsUser.Add(forbidden);
                viewModel.AllForbiddenWordsUser.Add(forbidden);
            }
            foreach (var word in forbiddenWordStandart)
            {
                var forbidden = new ForbiddenWordUser();
                forbidden.Word = word.Word;
                viewModel.ForbiddenWordsStandart.Add(forbidden);
                viewModel.AllForbiddenWordsUser.Add(forbidden);
            }

            List<string> arrayForbidden = new List<string>();
            foreach (var w in viewModel.AllForbiddenWordsUser)
            {
                arrayForbidden.Add(w.Word.ToLower());
            }

            string pattern = "\\b" + string.Join("\\b|\\b", arrayForbidden) + "\\b";
            string replace = "***";
            textForReplace = Regex.Replace(textForReplace, pattern, replace, RegexOptions.IgnoreCase);
            return textForReplace;
        }
    }
}