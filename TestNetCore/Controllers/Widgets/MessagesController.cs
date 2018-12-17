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
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace TestNetCore.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;

        public MessagesController(
            IHttpContextAccessor httpContextAccessor, 
            ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment)
            : base(httpContextAccessor, dbContext)
        {
            _appEnvironment = appEnvironment;
        }


        [HttpGet]
        [Route("messages")]
        public IActionResult Messages()
        {
            var viewModel = new MessagesViewModel();

            // FOR READ FROM TXT FILE AND WRITE TO DATABASE
            if (_dbContext.RussianDictionaries.FirstOrDefault() == null)
            {
                DefaultDataDB seed = new DefaultDataDB(_dbContext);
                seed.SeedForbidden();
            }

            viewModel = ModelForMessages(viewModel);
            return View("~/Views/Widgets/Messages.cshtml", viewModel);
        }


        public MessagesViewModel ModelForMessages(MessagesViewModel viewModel)
        {
            // Проверяю или существует директория для файлов для этого юзера, если нет - создаю
            string directoryFilesImg = $"wwwroot/usersfiles/{UserID}/imgForEmail/";
            if (!Directory.Exists(directoryFilesImg))
            {
                Directory.CreateDirectory(directoryFilesImg);
            }

            string directoryFilesVoice = $"wwwroot/usersfiles/{UserID}/voiceMessage/";
            if (!Directory.Exists(directoryFilesVoice))
            {
                Directory.CreateDirectory(directoryFilesVoice);
            }

            var isForbidden = _dbContext.ForbiddenWordUsers.FirstOrDefault(a => a.UserId == UserID);
            if (isForbidden != null)
            {
                var forbidden = _dbContext.ForbiddenWordUsers.Where(a => a.UserId == UserID);
                foreach (var f in forbidden)
                {
                    viewModel.StringForbiddenWordsUser = viewModel.StringForbiddenWordsUser + " " + f.Word;
                }
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
                        viewModel.AttachFile = file.FileName;
                    }
                }
                viewModel.AlertType = "alertUpload";
            }

            if (viewModel.PostType == "sendEmail")
            {
                // Create List Forbidden Words (Standart Forbidden Words + User )
                var forbiddenWordsUser = _dbContext.ForbiddenWordUsers.Where(u => u.UserId == UserID);
                var forbiddenWordStandart = _dbContext.ForbiddenWords.Where(a => a.Word != null);

                // for Change forbidden words to ***
                if (viewModel.TextMessage != null)
                {
                    viewModel.TextMessage = RemoveWords(viewModel.TextMessage, forbiddenWordsUser, forbiddenWordStandart, viewModel, "***");
                }
                SendEMail(UserEmail, viewModel.NameFrom, viewModel.TitleMessage, viewModel.TextMessage, viewModel.AttachFile, UserID);

                viewModel.AlertType = "alertSended";

                // write to DB
                EmailMessage mes = new EmailMessage();
                mes.UserId = UserID;
                mes.SendTo = UserEmail;
                mes.NameFrom = viewModel.NameFrom;
                mes.TitleMessage = viewModel.TitleMessage;
                mes.TextMessage = viewModel.TextMessage;
                mes.AttachFile = viewModel.AttachFile;
                mes.VoiceName = GetMd5Hash(viewModel.TextMessage) + ".mp3";
                mes.DateMessage = DateTime.Now;

                _dbContext.EmailMessages.Add(mes);
                _dbContext.SaveChanges();
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

                viewModel.AlertType = "alertSave";
            }

            viewModel = ModelForMessages(viewModel);

            return View("~/Views/Widgets/Messages.cshtml", viewModel);
        }

        //Save Text OnBlur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateVoice(string text)
        {
            if (text != null && text != "")
            {
                var generateMP3 = new Generate();
                var dirPath = $"wwwroot/usersfiles/{UserID}/voiceMessage/";

                // Create List Forbidden Words (Standart Forbidden Words + User )
                var forbiddenWordsUser = _dbContext.ForbiddenWordUsers.Where(u => u.UserId == UserID);
                var forbiddenWordStandart = _dbContext.ForbiddenWords.Where(a => a.Word != null);
                
                // for Change forbidden words to " цензура "
                MessagesViewModel viewModel = new MessagesViewModel();
                text = RemoveWords(text, forbiddenWordsUser, forbiddenWordStandart, viewModel, " цензура ");

                string fileName = GetMd5Hash(text);
                generateMP3.tts(text, "ru", dirPath, fileName);
                string pathForPlay = $"../usersfiles/{UserID}/voiceMessage/{fileName}.mp3";

                return Json(pathForPlay);
            }
            else
            {
                return Json("noVoice");
            }            
        }


        // Send Email  
        public static void SendEMail(string mailto, string name, string title, string message, string attachFile, string id)
        {
            try
            {
                MailMessage mail = new MailMessage();
                if(name == null)
                {
                    mail.From = new MailAddress("alexdotnetapp@gmail.com", "Message from test site: testdotnetapp.pp.ua)");
                }
                else
                {
                    mail.From = new MailAddress("alexdotnetapp@gmail.com", name + ", (from test site: testdotnetapp.pp.ua)");
                }
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
                                    MessagesViewModel viewModel,
                                    string replaceTo)
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
            string newText = Regex.Replace(textForReplace, pattern, replaceTo, RegexOptions.IgnoreCase);
            return newText;
        }


        // для формировании имени голосового файла исходя из текста сообщения
        public string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }
    }
}