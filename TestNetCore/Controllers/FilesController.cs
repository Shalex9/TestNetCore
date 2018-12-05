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
using TestNetCore.Models.Files;
using TestNetCore.Models.WidgetsViewModels;
using System.IO;

namespace TestNetCore.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private IMemoryCache cache;

        public FilesController(
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
        public IActionResult Gallery()
        {
            var viewModel = new GalleryViewModel();

            viewModel = ModelForGallery(viewModel);
            return View(viewModel);
        }


        public GalleryViewModel ModelForGallery(GalleryViewModel viewModel)
        {
            viewModel.UserId = UserID;

            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;


            var settingsDonatMessage = _dbContext.SettingsGalleryPages.FirstOrDefault(a => a.UserId == UserID);
            if (settingsDonatMessage != null)
            {
                viewModel.BgBox = settingsDonatMessage.BgBox;
                viewModel.BgMessage = settingsDonatMessage.BgMessage;
                viewModel.BgMessageOpacity = settingsDonatMessage.BgMessageOpacity;
                viewModel.AnimationDuration = settingsDonatMessage.AnimationDuration;
                viewModel.AnimationDelay = settingsDonatMessage.AnimationDelay;
                viewModel.LayoutMessage = settingsDonatMessage.LayoutMessage;
                
                viewModel.NameImage = settingsDonatMessage.NameImage;
                viewModel.PathImage = settingsDonatMessage.PathImage;
                viewModel.StartEffectAnimation = settingsDonatMessage.StartEffectAnimation;
                viewModel.FinishEffectAnimation = settingsDonatMessage.FinishEffectAnimation;
                
                viewModel.NameSound = settingsDonatMessage.NameSound;
                viewModel.PathSound = settingsDonatMessage.PathSound;
                viewModel.SoundVolume = settingsDonatMessage.SoundVolume;

                viewModel.StartEffectAnimation = settingsDonatMessage.StartEffectAnimation;
                viewModel.FinishEffectAnimation = settingsDonatMessage.FinishEffectAnimation;
                viewModel.CaliberImage = settingsDonatMessage.CaliberImage;

                viewModel.VerifyChanges = false;
                viewModel.UserId = UserID;
            }
            else
            {
                viewModel.BgBox = "00ff00";
                viewModel.BgMessage = "ff0000";
                viewModel.BgMessageOpacity = 1;
                viewModel.AnimationDuration = 20;
                viewModel.AnimationDelay = 2;
                viewModel.LayoutMessage = "above";
                
                viewModel.NameImage = "spongebob.gif";
                viewModel.PathImage = "/gallery/galleryImg/spongebob.gif";
                viewModel.StartEffectAnimation = "fadeInLeft";
                viewModel.FinishEffectAnimation = "fadeOutRight";
                viewModel.CaliberImage = 1;
                
                viewModel.NameSound = "default.ogg";
                viewModel.PathSound = "/gallery/gallerySound/default.ogg";
                viewModel.SoundVolume = 70;
                
                viewModel.StartEffectAnimation = "fadeInLeft";
                viewModel.FinishEffectAnimation = "fadeOutRight";

                viewModel.VerifyChanges = false;

                SettingsPageGallery defaultSettingsToDB;
                defaultSettingsToDB = new SettingsPageGallery();
                
                defaultSettingsToDB.BgBox = "00ff00";
                defaultSettingsToDB.BgMessage = "ff0000";
                defaultSettingsToDB.BgMessageOpacity = 1;
                defaultSettingsToDB.AnimationDuration = 20;
                defaultSettingsToDB.AnimationDelay = 2;
                defaultSettingsToDB.LayoutMessage = "above";
                
                defaultSettingsToDB.NameImage = "spongebob.gif";
                defaultSettingsToDB.PathImage = "/gallery/galleryImg/spongebob.gif";
                defaultSettingsToDB.StartEffectAnimation = "fadeInLeft";
                defaultSettingsToDB.FinishEffectAnimation = "fadeOutRight";
                defaultSettingsToDB.CaliberImage = 1;
                
                defaultSettingsToDB.NameSound = "default.ogg";
                defaultSettingsToDB.PathSound = "/gallery/gallerySound/default.ogg";
                defaultSettingsToDB.SoundVolume = 70;

                defaultSettingsToDB.StartEffectAnimation = "fadeInLeft";
                defaultSettingsToDB.FinishEffectAnimation = "fadeOutRight";

                defaultSettingsToDB.UserId = UserID;

                _dbContext.SettingsGalleryPages.Add(defaultSettingsToDB);
                _dbContext.SaveChanges();
            }



            foreach (GalleryFileImg galleryImg in _dbContext.GalleryFilesImgs)
            {
                var gi = new GalleryFileImg();
                gi.UserId = UserID;
                gi.Id = galleryImg.Id;
                gi.FileName = galleryImg.FileName;
                gi.Size = galleryImg.Size;
                viewModel.GalleryItemImgs.Add(gi);
            }

            foreach (GalleryFileSound gallerySound in _dbContext.GalleryFilesSounds)
            {
                var gi = new GalleryFileSound();
                gi.UserId = UserID;
                gi.Id = gallerySound.Id;
                gi.FileName = gallerySound.FileName;
                gi.Size = gallerySound.Size;
                viewModel.GalleryItemSounds.Add(gi);
            }

            foreach (UploadFileImg uploadImg in _dbContext.UploadFilesImgs)
            {
                var gi = new UploadFileImg();
                gi.UserId = UserID;
                gi.Id = uploadImg.Id;
                gi.FileName = uploadImg.FileName;
                gi.Size = uploadImg.Size;
                viewModel.UploadItemImgs.Add(gi);
            }

            foreach (UploadFileSound uploadSound in _dbContext.UploadFilesSounds)
            {
                var gi = new UploadFileSound();
                gi.UserId = UserID;
                gi.Id = uploadSound.Id;
                gi.FileName = uploadSound.FileName;
                gi.Size = uploadSound.Size;
                viewModel.UploadItemSounds.Add(gi);
            }

            viewModel.ImageGalleryCount = _dbContext.GalleryFilesImgs.Count();
            viewModel.SoundGalleryCount = _dbContext.GalleryFilesSounds.Count();
            viewModel.ImageUploadCount = _dbContext.UploadFilesImgs.Count();
            viewModel.SoundUploadCount = _dbContext.UploadFilesSounds.Count();

            return viewModel;
        }



        [HttpPost]
        public IActionResult Gallery(List<IFormFile> files, GalleryViewModel viewModel)
        {
            if (viewModel.PostUpload == "uploadFiles")
            {
                viewModel.GalleryVisible = true;
                if (viewModel.TypeUploadFile == "image")
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var token = UserID.ToString();
                            var fileName = Path.GetFileName(file.FileName);
                            var dirName = "wwwroot/gallery/uploadImg/" + token + "/";
                            if (! Directory.Exists(dirName))
                            {
                                var userDir = Directory.CreateDirectory(dirName);
                            }
                            
                            var path = Path.Combine(dirName, fileName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                        }
                        var uploadFile = new UploadFileImg();
                        uploadFile.UserId = UserID;
                        uploadFile.FileName = file.FileName;
                        uploadFile.Size = (file.Length / 1024) + "КБ";

                        _dbContext.UploadFilesImgs.Add(uploadFile);
                        _dbContext.SaveChanges();
                    }
                }
                if (viewModel.TypeUploadFile == "audio")
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var token = UserID.ToString();
                            var fileName = Path.GetFileName(file.FileName);
                            var dirName = "wwwroot/gallery/uploadSound/" + token + "/";
                            if (!Directory.Exists(dirName))
                            {
                                var userDir = Directory.CreateDirectory(dirName);
                            }

                            var path = Path.Combine(dirName, fileName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                        }
                        var uploadFile = new UploadFileSound();
                        uploadFile.UserId = UserID;
                        uploadFile.FileName = file.FileName;
                        uploadFile.Size = (file.Length / 1024) + "КБ";

                        _dbContext.UploadFilesSounds.Add(uploadFile);
                        _dbContext.SaveChanges();
                    }
                }
            }


            // for Delete File in Gallery
            var delImage = _dbContext.UploadFilesImgs.FirstOrDefault(v => v.Id == viewModel.IdForDeletionImage);
            if (delImage != null)
            {
                _dbContext.UploadFilesImgs.Remove(delImage);
                _dbContext.SaveChanges();
            }

            var delSound = _dbContext.UploadFilesSounds.FirstOrDefault(v => v.Id == viewModel.IdForDeletionSound);
            if (delSound != null)
            {
                _dbContext.UploadFilesSounds.Remove(delSound);
                _dbContext.SaveChanges();
            }


            if (viewModel.PostSaveSettings == "saveSettings")
            {
                var settGallery = _dbContext.SettingsGalleryPages.FirstOrDefault(a => a.UserId == UserID);
                SettingsPageGallery settings;
                bool exists = false;
                if (settGallery == null)
                {
                    settings = new SettingsPageGallery();
                }
                else
                {
                    settings = settGallery;
                    exists = true;
                }
                var token = UserID.ToString();

                // for Save New Settings
                settings.UserId = UserID;
                
                settings.BgBox = viewModel.BgBox;
                settings.BgMessage = viewModel.BgMessage;
                settings.BgMessageOpacity = viewModel.BgMessageOpacity;
                settings.AnimationDuration = viewModel.AnimationDuration;
                settings.AnimationDelay = viewModel.AnimationDelay;
                settings.LayoutMessage = viewModel.LayoutMessage;
                
                settings.NameImage = viewModel.NameImage;
                if (viewModel.SelectImageSource == "galleryImg")
                { settings.PathImage = "/gallery/galleryImg/" + viewModel.NameImage; }
                if (viewModel.SelectImageSource == "uploadImg")
                { settings.PathImage = "/gallery/uploadImg/" + token + "/" + viewModel.NameImage; }
                settings.StartEffectAnimation = viewModel.StartEffectAnimation;
                settings.FinishEffectAnimation = viewModel.FinishEffectAnimation;
                settings.CaliberImage = viewModel.CaliberImage;
                
                settings.NameSound = viewModel.NameSound;
                if (viewModel.SelectSoundSource == "gallerySound")
                { settings.PathSound = "/gallery/gallerySound/"+ viewModel.NameSound; }
                if (viewModel.SelectSoundSource == "uploadSound")
                { settings.PathSound = "/gallery/uploadSound/" + token + "/" + viewModel.NameSound; }
                settings.SoundVolume = viewModel.SoundVolume;
                
                settings.StartEffectAnimation = viewModel.StartEffectAnimation;
                settings.FinishEffectAnimation = viewModel.FinishEffectAnimation;

                if (exists)
                {
                    _dbContext.SettingsGalleryPages.Update(settings);
                }
                else
                {
                    _dbContext.SettingsGalleryPages.Add(settings);
                }

                _dbContext.SaveChanges();
            }

            var vm = ModelForGallery(viewModel);
            vm.VerifyChanges = true;
            return View(vm);
        }



        [HttpGet]
        public IActionResult Crud()
        {
            var viewModel = new CrudViewModel();

            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return View(viewModel);
        }


        public IActionResult GetResume()
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "TestFiles/CV - Alexander Shevchuk.pdf");
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return PhysicalFile(file_path, file_type, file_name);
        }


        public FileResult GetStream()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "TestFiles/CV - Alexander Shevchuk.pdf");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "CV - Alexander Shevchuk.pdf";
            return File(fs, file_type, file_name);
        }



        [HttpGet]
        public IActionResult Dictionary()
        {
            var viewModel = new DictionaryViewModel();
            viewModel = ModelForDictionary(viewModel);

            return View(viewModel);
        }

        public DictionaryViewModel ModelForDictionary(DictionaryViewModel viewModel)
        {
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            viewModel.CountWord = _dbContext.RussianDictionaries.Count();
            return viewModel;
        }

        [HttpPost]
        public IActionResult Dictionary(DictionaryViewModel viewModel)
        {
            // FOR READ FROM TXT FILE AND WRITE TO DATABASE
            //WordSeeder seed = new WordSeeder(_dbContext);
            //seed.SeedWord();
            //seed.SeedDictionary();
            //seed.SeedForbidden();

            var vm = ModelForDictionary(viewModel);
            return View(vm);
        }

        // Получение искомого слова
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchWord(string word)
        {
            try
            {
                RussianDictionary w = _dbContext.RussianDictionaries.FirstOrDefault(a => a.Word == word);
                return Json(w.Definition);
            }
            catch (Exception)
            {
                return Json("Неккоректно введено слово, или по введенному слову данных нет.");
            }
        }

        // Получение ассоциации
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchAssotiation(string word)
        {
            try
            {
                int wLen = word.Length;
                char lastSymbol = word[wLen - 1];
                string wordСonsonants =word.Trim().ToLower().Replace("а", "")
                                                            .Replace("о", "")
                                                            .Replace("у", "")
                                                            .Replace("и", "")
                                                            .Replace("е", "")
                                                            .Replace("э", "")
                                                            .Replace("ю", "")
                                                            .Replace("я", "")
                                                            //.Replace("й", "")
                                                            .Replace("ы", "");
                string f3consL = String.Concat(wordСonsonants[0], wordСonsonants[1], wordСonsonants[2]);
                string f3l = word.Substring(0, 3);

                IQueryable<RussianDictionary> list3letters = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, f3l + "%"));
                IQueryable<RussianDictionary> list3lettersAndLastSymbol = list3letters.Where(a => EF.Functions.Like(a.Word, "%["+ lastSymbol + "]"));
                IQueryable<RussianDictionary> list3ConsLetters = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, f3consL + "%"));

                IQueryable<RussianDictionary> listCategory1 = _dbContext.RussianDictionaries.Where(a => EF.Functions.Like(a.Word, word + "%"));
                IQueryable<RussianDictionary> listCategory2 = list3letters.Where(a => a.Word.Length == wLen || a.Word.Length == wLen - 1 || a.Word.Length == wLen + 1);
                IQueryable<RussianDictionary> listCategory3 = listCategory2.Where(a => EF.Functions.Like(a.Word, "%[" + lastSymbol + "]"));
                IQueryable<RussianDictionary> listCategory4 = list3ConsLetters.Where(a => a.Word.Length == wLen || a.Word.Length == wLen - 1 || a.Word.Length == wLen + 1);
                IQueryable<RussianDictionary> listCategory5 = listCategory4.Where(a => EF.Functions.Like(a.Word, "%[" + lastSymbol + "]"));

                AssociatedWords listAssociation = new AssociatedWords();
                listAssociation.Category1 = listCategory1;
                listAssociation.Category2 = listCategory2;
                listAssociation.Category3 = listCategory3;
                listAssociation.Category4 = listCategory4;
                listAssociation.Category5 = listCategory5;
                //listAssociation.Category6 = listCategory6;
                //listAssociation.Category7 = listCategory7;

                return Json(listAssociation);
            }
            catch (Exception)
            {
                return Json("Неккоректно введено слово, или по введенному слову данных нет.");
            }
        }


        // Сохранение слов-ассоциаций
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssociation(string word, string wordAssociation)
        {
            IQueryable<SavedUserAssociation> userAss = _dbContext.SavedUserAssociations.Where(a => a.UserId == UserID);
            SavedUserAssociation isWord = userAss.FirstOrDefault(a => a.Word == word);
            SavedUserAssociation newAss;

            if (isWord != null)
            {
                newAss = isWord;
                newAss.AllWordsAss = String.Concat(newAss.AllWordsAss, "; ", wordAssociation);
                _dbContext.SavedUserAssociations.Update(newAss);
            }
            else
            {
                newAss = new SavedUserAssociation();
                newAss.UserId = UserID;
                newAss.Word = word;
                newAss.AllWordsAss = wordAssociation;
                _dbContext.SavedUserAssociations.Add(newAss);
            }

            _dbContext.SaveChanges();
            return Json("Success");
        }

    }
}
