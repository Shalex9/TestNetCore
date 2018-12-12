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
using TestNetCore.Models.Files.Gallery;
using System.IO;
using TestNetCore.Data.TableData;

namespace TestNetCore.Controllers.Files
{
    public class GalleryController : BaseController
    {
        public GalleryController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        { }


        [HttpGet]
        [Route("gallery")]
        public IActionResult Gallery()
        {
            var viewModel = new GalleryViewModel();

            viewModel = ModelForGallery(viewModel);
            return View("~/Views/Files/Gallery.cshtml", viewModel);
        }


        public GalleryViewModel ModelForGallery(GalleryViewModel viewModel)
        {
            viewModel.UserId = UserID;
            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            viewModel = ReadSettingsFromDbOrWriteDefault(viewModel);
            viewModel = ReadGalleryFilesFromDB(viewModel);

            return viewModel;
        }
        
        public GalleryViewModel ReadSettingsFromDbOrWriteDefault(GalleryViewModel viewModel)
        {
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

            return viewModel;
        }

        public GalleryViewModel ReadGalleryFilesFromDB(GalleryViewModel viewModel)
        {
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

            foreach (UploadFileImg uploadImg in _dbContext.UploadFilesImgs.Where(a => a.UserId == UserID))
            {
                var gi = new UploadFileImg();
                gi.UserId = UserID;
                gi.Id = uploadImg.Id;
                gi.FileName = uploadImg.FileName;
                gi.Size = uploadImg.Size;
                viewModel.UploadItemImgs.Add(gi);
            }

            foreach (UploadFileSound uploadSound in _dbContext.UploadFilesSounds.Where(a => a.UserId == UserID))
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
            viewModel.ImageUploadCount = _dbContext.UploadFilesImgs.Where(a => a.UserId == UserID).Count();
            viewModel.SoundUploadCount = _dbContext.UploadFilesSounds.Where(a => a.UserId == UserID).Count();

            return viewModel;
        }

        [HttpPost]
        [Route("gallery")]
        public IActionResult Gallery(List<IFormFile> files, GalleryViewModel viewModel)
        {
            if (viewModel.PostType == "uploadFiles")
            {
                viewModel = MethodUploadFiles(files, viewModel);
            }


            if (viewModel.PostType == "deleteFiles")
            {
                MethodDeleteFiles(viewModel.IdForDeletionImage, viewModel.IdForDeletionSound);
            }

            
            if (viewModel.PostType == "saveSettings")
            {
                viewModel = MethodSaveSettings(viewModel);
            }

            viewModel = ModelForGallery(viewModel);
            viewModel.VerifyChanges = true;
            return View("~/Views/Files/Gallery.cshtml", viewModel);
        }

        public GalleryViewModel MethodUploadFiles (List<IFormFile> files, GalleryViewModel viewModel)
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
            return viewModel;
        }

        public GalleryViewModel MethodSaveSettings(GalleryViewModel viewModel)
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
            { settings.PathSound = "/gallery/gallerySound/" + viewModel.NameSound; }
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

            return viewModel;
        }

        public void MethodDeleteFiles(int? idImage, int? idSound)
        {
            var delImage = _dbContext.UploadFilesImgs.FirstOrDefault(v => v.Id == idImage);
            if (delImage != null)
            {
                _dbContext.UploadFilesImgs.Remove(delImage);
                _dbContext.SaveChanges();
            }

            var delSound = _dbContext.UploadFilesSounds.FirstOrDefault(v => v.Id == idSound);
            if (delSound != null)
            {
                _dbContext.UploadFilesSounds.Remove(delSound);
                _dbContext.SaveChanges();
            }
        }
    }
}
