using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestNetCore.Data;
using TestNetCore.Models.WidgetsViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using TestNetCore.Models;
using Newtonsoft.Json;

namespace TestNetCore.Controllers
{
    public class WidgetsController : BaseController
    {
        public WidgetsController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
            : base(httpContextAccessor, dbContext)
        {
        }

        // Alert Box (оповещение о донате)
        [HttpGet]
        public IActionResult DonatNotification()
        {
            var viewModel = new DonatViewModel();
            viewModel.GalleryVisible = false;

            viewModel = CreateModelForDonatNotification(viewModel);
            viewModel.VerifyChanges = false;
            return View(viewModel);
        }

        public DonatViewModel CreateModelForDonatNotification(DonatViewModel viewModel)
        {
            var nameThisWidget = "alertBox";
            var stringIdUser = UserID.ToString().Replace("-", "");
            var sitePath = _httpContextAccessor.HttpContext.Request.Host;
            var linkWidget = "https://" + sitePath + "/widget/" + nameThisWidget + "/" + stringIdUser;

            var userWidget = _dbContext.AllUserWidgets.FirstOrDefault(a => a.UserId == Guid.Parse(UserID));

            if (userWidget == null)
            {
                AllUserWidget setWidget;
                setWidget = new AllUserWidget();
                setWidget.UserId = Guid.Parse(UserID);
                setWidget.RunTestMessage = false;
                setWidget.WidgetUrl = linkWidget;

                _dbContext.AllUserWidgets.Add(setWidget);
                _dbContext.SaveChanges();
            }

            viewModel.LinkDonation = _dbContext.AllUserWidgets.FirstOrDefault(a => a.UserId == Guid.Parse(UserID)).WidgetUrl;

            var settingsDonatMessage = _dbContext.SettingsDonationNotifications.FirstOrDefault(a => a.UserId == UserID);
            if (settingsDonatMessage != null)
            {
                viewModel.AnimationVisibleDon = settingsDonatMessage.AnimationVisibleDon;
                viewModel.BgBoxDon = settingsDonatMessage.BgBoxDon;
                viewModel.BgMessageDon = settingsDonatMessage.BgMessageDon;
                viewModel.BgMessageOpacityDon = settingsDonatMessage.BgMessageOpacityDon;
                viewModel.AnimationDurationDon = settingsDonatMessage.AnimationDurationDon;
                viewModel.AnimationDelayDon = settingsDonatMessage.AnimationDelayDon;
                viewModel.LayoutMessageDon = settingsDonatMessage.LayoutMessageDon;

                viewModel.AnimationImageVisibleDon = settingsDonatMessage.AnimationImageVisibleDon;
                viewModel.NameImageDon = settingsDonatMessage.NameImageDon;
                viewModel.PathImageDon = settingsDonatMessage.PathImageDon;
                viewModel.StartEffectAnimationDon = settingsDonatMessage.StartEffectAnimationDon;
                viewModel.FinishEffectAnimationDon = settingsDonatMessage.FinishEffectAnimationDon;

                viewModel.SoundVisibleDon = settingsDonatMessage.SoundVisibleDon;
                viewModel.NameSoundDon = settingsDonatMessage.NameSoundDon;
                viewModel.PathSoundDon = settingsDonatMessage.PathSoundDon;
                viewModel.SoundVolumeDon = settingsDonatMessage.SoundVolumeDon;

                viewModel.MinAmountDon = settingsDonatMessage.MinAmountDon;
                viewModel.TextDelayDon = settingsDonatMessage.TextDelayDon;
                viewModel.TemplateTextDon = settingsDonatMessage.TemplateTextDon;
                viewModel.StartEffectAnimationTextDon = settingsDonatMessage.StartEffectAnimationTextDon;
                viewModel.FinishEffectAnimationTextDon = settingsDonatMessage.FinishEffectAnimationTextDon;
                viewModel.CaliberImageDon = settingsDonatMessage.CaliberImageDon;

                viewModel.FontFamilyTitleDon = settingsDonatMessage.FontFamilyTitleDon;
                viewModel.FontColorTitleDon = settingsDonatMessage.FontColorTitleDon;
                viewModel.FontColorTitleOpacityDon = settingsDonatMessage.FontColorTitleOpacityDon;
                viewModel.FontHighlightColorTitleDon = settingsDonatMessage.FontHighlightColorTitleDon;
                viewModel.FontSizeTitleDon = settingsDonatMessage.FontSizeTitleDon;
                viewModel.FontWeightTitleDon = settingsDonatMessage.FontWeightTitleDon;
                viewModel.FontItalicTitleDon = settingsDonatMessage.FontItalicTitleDon;
                viewModel.FontUnderlineTitleDon = settingsDonatMessage.FontUnderlineTitleDon;
                viewModel.LetterSpacingTitleDon = settingsDonatMessage.LetterSpacingTitleDon;
                viewModel.WordSpacingTitleDon = settingsDonatMessage.WordSpacingTitleDon;
                viewModel.ShadowTitleDon = settingsDonatMessage.ShadowTitleDon;
                viewModel.FontAnimationTitleDon = settingsDonatMessage.FontAnimationTitleDon;

                viewModel.FontFamilyTextDon = settingsDonatMessage.FontFamilyTextDon;
                viewModel.FontColorTextDon = settingsDonatMessage.FontColorTextDon;
                viewModel.FontColorTextOpacityDon = settingsDonatMessage.FontColorTextOpacityDon;
                viewModel.FontSizeTextDon = settingsDonatMessage.FontSizeTextDon;
                viewModel.FontWeightTextDon = settingsDonatMessage.FontWeightTextDon;
                viewModel.FontItalicTextDon = settingsDonatMessage.FontItalicTextDon;
                viewModel.FontUnderlineTextDon = settingsDonatMessage.FontUnderlineTextDon;
                viewModel.LetterSpacingTextDon = settingsDonatMessage.LetterSpacingTextDon;
                viewModel.WordSpacingTextDon = settingsDonatMessage.WordSpacingTextDon;
                viewModel.ShadowTextDon = settingsDonatMessage.ShadowTextDon;
                viewModel.FontAnimationTextDon = settingsDonatMessage.FontAnimationTextDon;
                viewModel.VerifyChanges = false;
                viewModel.UserId = UserID;
            }
            else
            {
                viewModel.AnimationVisibleDon = true;
                viewModel.BgBoxDon = "00ff00";
                viewModel.BgMessageDon = "ff0000";
                viewModel.BgMessageOpacityDon = 1;
                viewModel.AnimationDurationDon = 20;
                viewModel.AnimationDelayDon = 2;
                viewModel.LayoutMessageDon = "above";

                viewModel.AnimationImageVisibleDon = true;
                viewModel.NameImageDon = "spongebob.gif";
                viewModel.PathImageDon = "/gallery/galleryImg/spongebob.gif";
                viewModel.StartEffectAnimationDon = "fadeInLeft";
                viewModel.FinishEffectAnimationDon = "fadeOutRight";
                viewModel.CaliberImageDon = 1;

                viewModel.SoundVisibleDon = true;
                viewModel.NameSoundDon = "default.ogg";
                viewModel.PathSoundDon = "/gallery/gallerySound/default.ogg";
                viewModel.SoundVolumeDon = 70;

                viewModel.MinAmountDon = 0;
                viewModel.TextDelayDon = 2;
                viewModel.TemplateTextDon = "{name} Примите платеж {amount}!";
                viewModel.StartEffectAnimationTextDon = "fadeInLeft";
                viewModel.FinishEffectAnimationTextDon = "fadeOutRight";

                viewModel.FontFamilyTitleDon = "Arial";
                viewModel.FontColorTitleDon = "ffffff";
                viewModel.FontColorTitleOpacityDon = 95;
                viewModel.FontHighlightColorTitleDon = "000000";
                viewModel.FontSizeTitleDon = "30px";
                viewModel.FontWeightTitleDon = "normal";
                viewModel.FontItalicTitleDon = "normal";
                viewModel.FontUnderlineTitleDon = "none";
                viewModel.LetterSpacingTitleDon = "1px";
                viewModel.WordSpacingTitleDon = "1px";
                viewModel.ShadowTitleDon = "1px";
                viewModel.FontAnimationTitleDon = "vibe";

                viewModel.FontFamilyTextDon = "Arial";
                viewModel.FontColorTextDon = "000000";
                viewModel.FontColorTextOpacityDon = 95;
                viewModel.FontSizeTextDon = "14px";
                viewModel.FontWeightTextDon = "normal";
                viewModel.FontItalicTextDon = "normal";
                viewModel.FontUnderlineTextDon = "none";
                viewModel.LetterSpacingTextDon = "1px";
                viewModel.WordSpacingTextDon = "1px";
                viewModel.ShadowTextDon = "1px";
                viewModel.FontAnimationTextDon = "vibe";
                viewModel.VerifyChanges = false;

                SettingsDonationNotification defaultSettingsToDB;
                defaultSettingsToDB = new SettingsDonationNotification();
                defaultSettingsToDB.Token = UserID.Replace("-", "");

                defaultSettingsToDB.AnimationVisibleDon = true;
                defaultSettingsToDB.BgBoxDon = "00ff00";
                defaultSettingsToDB.BgMessageDon = "ff0000";
                defaultSettingsToDB.BgMessageOpacityDon = 1;
                defaultSettingsToDB.AnimationDurationDon = 20;
                defaultSettingsToDB.AnimationDelayDon = 2;
                defaultSettingsToDB.LayoutMessageDon = "above";

                defaultSettingsToDB.AnimationImageVisibleDon = true;
                defaultSettingsToDB.NameImageDon = "spongebob.gif";
                defaultSettingsToDB.PathImageDon = "/gallery/galleryImg/spongebob.gif";
                defaultSettingsToDB.StartEffectAnimationDon = "fadeInLeft";
                defaultSettingsToDB.FinishEffectAnimationDon = "fadeOutRight";
                defaultSettingsToDB.CaliberImageDon = 1;

                defaultSettingsToDB.SoundVisibleDon = true;
                defaultSettingsToDB.NameSoundDon = "default.ogg";
                defaultSettingsToDB.PathSoundDon = "/gallery/gallerySound/default.ogg";
                defaultSettingsToDB.SoundVolumeDon = 70;

                defaultSettingsToDB.MinAmountDon = 0;
                defaultSettingsToDB.TextDelayDon = 2;
                defaultSettingsToDB.TemplateTextDon = "{name} Примите платеж {amount}!";
                defaultSettingsToDB.StartEffectAnimationTextDon = "fadeInLeft";
                defaultSettingsToDB.FinishEffectAnimationTextDon = "fadeOutRight";

                defaultSettingsToDB.FontFamilyTitleDon = "Arial";
                defaultSettingsToDB.FontColorTitleDon = "ffffff";
                defaultSettingsToDB.FontColorTitleOpacityDon = 95;
                defaultSettingsToDB.FontHighlightColorTitleDon = "000000";
                defaultSettingsToDB.FontSizeTitleDon = "30px";
                defaultSettingsToDB.FontWeightTitleDon = "normal";
                defaultSettingsToDB.FontItalicTitleDon = "normal";
                defaultSettingsToDB.FontUnderlineTitleDon = "none";
                defaultSettingsToDB.LetterSpacingTitleDon = "1px";
                defaultSettingsToDB.WordSpacingTitleDon = "1px";
                defaultSettingsToDB.ShadowTitleDon = "1px";
                defaultSettingsToDB.FontAnimationTitleDon = "vibe";

                defaultSettingsToDB.FontFamilyTextDon = "Arial";
                defaultSettingsToDB.FontColorTextDon = "000000";
                defaultSettingsToDB.FontColorTextOpacityDon = 95;
                defaultSettingsToDB.FontSizeTextDon = "14px";
                defaultSettingsToDB.FontWeightTextDon = "normal";
                defaultSettingsToDB.FontItalicTextDon = "normal";
                defaultSettingsToDB.FontUnderlineTextDon = "none";
                defaultSettingsToDB.LetterSpacingTextDon = "1px";
                defaultSettingsToDB.WordSpacingTextDon = "1px";
                defaultSettingsToDB.ShadowTextDon = "1px";
                defaultSettingsToDB.FontAnimationTextDon = "vibe";

                defaultSettingsToDB.UserId = UserID;

                _dbContext.SettingsDonationNotifications.Add(defaultSettingsToDB);
                _dbContext.SaveChanges();
            }

            //viewModel.Currency = currencyUser;
            viewModel.UserId = UserID;

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


            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;
            return viewModel;
        }

        [HttpPost]
        public IActionResult DonatNotification(List<IFormFile> files, DonatViewModel viewModel)
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
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine("wwwroot/gallery/uploadImg/", fileName);
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
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine("wwwroot/gallery/uploadSound/", fileName);
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


            if (viewModel.PostSaveSettings == "saveSettings")
            {
                var setingMessageCurrentUser = _dbContext.SettingsDonationNotifications.FirstOrDefault(a => a.UserId == UserID);
                SettingsDonationNotification settingMes;
                bool exists = false;
                if (setingMessageCurrentUser == null)
                {
                    settingMes = new SettingsDonationNotification();
                }
                else
                {
                    settingMes = setingMessageCurrentUser;
                    exists = true;
                }

                // for Save New Settings
                settingMes.UserId = UserID;

                settingMes.AnimationVisibleDon = viewModel.AnimationVisibleDon;
                settingMes.BgBoxDon = viewModel.BgBoxDon;
                settingMes.BgMessageDon = viewModel.BgMessageDon;
                settingMes.BgMessageOpacityDon = viewModel.BgMessageOpacityDon;
                settingMes.AnimationDurationDon = viewModel.AnimationDurationDon;
                settingMes.AnimationDelayDon = viewModel.AnimationDelayDon;
                settingMes.LayoutMessageDon = viewModel.LayoutMessageDon;

                settingMes.AnimationImageVisibleDon = viewModel.AnimationImageVisibleDon;
                settingMes.NameImageDon = viewModel.NameImageDon;
                if (viewModel.SelectImageSource == "galleryImg")
                { settingMes.PathImageDon = "/gallery/galleryImg/" + viewModel.NameImageDon; }
                if (viewModel.SelectImageSource == "uploadImg")
                { settingMes.PathImageDon = "/gallery/uploadImg/" + viewModel.NameImageDon; }
                settingMes.StartEffectAnimationDon = viewModel.StartEffectAnimationDon;
                settingMes.FinishEffectAnimationDon = viewModel.FinishEffectAnimationDon;
                settingMes.CaliberImageDon = viewModel.CaliberImageDon;

                settingMes.SoundVisibleDon = viewModel.SoundVisibleDon;
                settingMes.NameSoundDon = viewModel.NameSoundDon;
                if (viewModel.SelectSoundSource == "gallerySound")
                { settingMes.PathSoundDon = "/gallery/gallerySound/" + viewModel.NameSoundDon; }
                if (viewModel.SelectSoundSource == "uploadSound")
                { settingMes.PathSoundDon = "/gallery/uploadSound/" + viewModel.NameSoundDon; }
                settingMes.SoundVolumeDon = viewModel.SoundVolumeDon;

                settingMes.MinAmountDon = viewModel.MinAmountDon;
                settingMes.TextDelayDon = viewModel.TextDelayDon;
                settingMes.TemplateTextDon = viewModel.TemplateTextDon;
                settingMes.StartEffectAnimationTextDon = viewModel.StartEffectAnimationTextDon;
                settingMes.FinishEffectAnimationTextDon = viewModel.FinishEffectAnimationTextDon;

                settingMes.FontFamilyTitleDon = viewModel.FontFamilyTitleDon;
                settingMes.FontColorTitleDon = viewModel.FontColorTitleDon;
                settingMes.FontColorTitleOpacityDon = viewModel.FontColorTitleOpacityDon;
                settingMes.FontHighlightColorTitleDon = viewModel.FontHighlightColorTitleDon;
                settingMes.FontSizeTitleDon = viewModel.FontSizeTitleDon;
                settingMes.FontWeightTitleDon = viewModel.FontWeightTitleDon;
                settingMes.FontItalicTitleDon = viewModel.FontItalicTitleDon;
                settingMes.FontUnderlineTitleDon = viewModel.FontUnderlineTitleDon;
                settingMes.LetterSpacingTitleDon = viewModel.LetterSpacingTitleDon;
                settingMes.WordSpacingTitleDon = viewModel.WordSpacingTitleDon;
                settingMes.ShadowTitleDon = viewModel.ShadowTitleDon;
                settingMes.FontAnimationTitleDon = viewModel.FontAnimationTitleDon;

                settingMes.FontFamilyTextDon = viewModel.FontFamilyTextDon;
                settingMes.FontColorTextDon = viewModel.FontColorTextDon;
                settingMes.FontColorTextOpacityDon = viewModel.FontColorTextOpacityDon;
                settingMes.FontSizeTextDon = viewModel.FontSizeTextDon;
                settingMes.FontWeightTextDon = viewModel.FontWeightTextDon;
                settingMes.FontItalicTextDon = viewModel.FontItalicTextDon;
                settingMes.FontUnderlineTextDon = viewModel.FontUnderlineTextDon;
                settingMes.LetterSpacingTextDon = viewModel.LetterSpacingTextDon;
                settingMes.WordSpacingTextDon = viewModel.WordSpacingTextDon;
                settingMes.ShadowTextDon = viewModel.ShadowTextDon;
                settingMes.FontAnimationTextDon = viewModel.FontAnimationTextDon;

                if (exists)
                {
                    _dbContext.SettingsDonationNotifications.Update(settingMes);
                }
                else
                {
                    _dbContext.SettingsDonationNotifications.Add(settingMes);
                }

                _dbContext.SaveChanges();

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

            var vm = CreateModelForDonatNotification(viewModel);
            vm.VerifyChanges = true;
            return View(vm);
        }



        [HttpGet]
        public IActionResult Hotel()
        {
            var viewModel = new HotelViewModel();
            viewModel = CreateModelForHotel(viewModel);
            viewModel.RoomReserved = false;

            return View(viewModel);
        }

        public HotelViewModel CreateModelForHotel(HotelViewModel viewModel)
        {
            DateTime now = DateTime.Now;

            CheckIsFreeRoomNow(viewModel);


            viewModel.ListRooms = _dbContext.HotelInformations.ToList();
            viewModel.ListAllReserved = _dbContext.HotelReservations.Where(u => u.EndReserv > now).ToList();
            viewModel.ListAllArhiveReserved = _dbContext.HotelReservations.Where(u => u.EndReserv < now).ToList();
            viewModel.ListUserReserved = _dbContext.HotelReservations.Where(u => u.GuestGuid == UserID && u.EndReserv > now).ToList();
            viewModel.ListUserArhiveReserved = _dbContext.HotelReservations.Where(u => u.GuestGuid == UserID && u.EndReserv < now).ToList();
            viewModel.List101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            viewModel.List102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            viewModel.List201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            viewModel.List202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            if (viewModel.ListUserReserved.Count() != 0)
                viewModel.HasUserReserve = true;
            else
                viewModel.HasUserReserve = false;

            if (viewModel.ListUserArhiveReserved.Count() != 0)
                viewModel.HasUserArhive = true;
            else
                viewModel.HasUserArhive = false;

            if (viewModel.ListAllArhiveReserved.Count() != 0)
                viewModel.HasArhive = true;
            else
                viewModel.HasArhive = false;


            viewModel.UserName = CurrentUserName;
            viewModel.AvatarPath = CurrentAvatarPath;

            return viewModel;
        }

        public HotelViewModel CheckIsFreeRoomNow(HotelViewModel viewModel)
        {
            DateTime now = DateTime.Now;
            viewModel.ListRooms = _dbContext.HotelInformations.ToList();

            var list101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            var list102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            var list201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            var list202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            var room101 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 101);
            var room102 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 102);
            var room201 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 201);
            var room202 = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 202);

            bool free101 = true;
            foreach (var i in list101Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    free101 = false;
                    break;
                }
                else
                {
                    free101 = true;
                }
            }
            if (free101)
            {
                room101.IsFreeNow = true;
            }
            else
            {
                room101.IsFreeNow = false;
            }

            bool free102 = true;
            foreach (var i in list102Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    free102 = false;
                    break;
                }
                else
                {
                    free102 = true;
                }
            }
            if (free102)
            {
                room102.IsFreeNow = true;
            }
            else
            {
                room102.IsFreeNow = false;
            }

            bool free201 = true;
            foreach (var i in list201Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    free201 = false;
                    break;
                }
                else
                {
                    free201 = true;
                }
            }
            if (free201)
            {
                room201.IsFreeNow = true;
            }
            else
            {
                room201.IsFreeNow = false;
            }

            bool free202 = true;
            foreach (var i in list202Reserved)
            {
                if (now > i.StartReserv && now < i.EndReserv)
                {
                    free202 = false;
                    break;
                }
                else
                {
                    free202 = true;
                }
            }
            if (free202)
            {
                room202.IsFreeNow = true;
            }
            else
            {
                room202.IsFreeNow = false;
            }


            _dbContext.HotelInformations.Update(room101);
            _dbContext.HotelInformations.Update(room102);
            _dbContext.HotelInformations.Update(room201);
            _dbContext.HotelInformations.Update(room202);

            _dbContext.SaveChanges();

            return viewModel;
        }

        [HttpPost]
        public IActionResult Hotel(HotelViewModel viewModel)
        {
            var vm = CreateModelForHotel(viewModel);
            var room = viewModel.NumberOfRoom;
            var price = _dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == room).PriceForRoom;
            var countDay = (viewModel.EndReserv - viewModel.StartReserv).Days + 1;
            var summ = countDay * price;
            vm.SummReserv = summ;
            var textToEmail = "Вы успешно забронировали номер! " +
                "Информация о Вашей брони: Номер комнаты:" + room + ". Тип номера: " + ". " +
                "Дата заезда: " + viewModel.StartReserv + ". Дата отъезда: " + viewModel.EndReserv +
                ". Всего по оплате: " + summ + ". " +
                "Ждем Вас в нашем отеле и желаем счастливого отдыха!";

            var reserve = new HotelReservation();

            reserve.NumberOfRoom = viewModel.NumberOfRoom;
            reserve.StartReserv = viewModel.StartReserv;
            reserve.EndReserv = viewModel.EndReserv;
            reserve.DateReserv = DateTime.Now;
            reserve.GuestGuid = UserID;
            reserve.GuestEmail = UserEmail;
            reserve.SummReserv = summ;
            reserve.GuestName = _dbContext.ClaimsDataUsers.FirstOrDefault(u => u.UserEmail == UserEmail.ToString()).UserName;

            _dbContext.HotelReservations.Add(reserve);
            _dbContext.SaveChanges();

            //DateTime now = DateTime.Now;
            //vm.ListUserReserved = _dbContext.HotelReservations.Where(u => u.GuestGuid == UserID && u.EndReserv > now).ToList();

            //if (vm.ListUserReserved.Count() != 0)
            //    vm.HasUserReserve = true;
            //else
            //    vm.HasUserReserve = false;

            //var mes = SendMessage();

            vm.ListAllReserved.Add(reserve);
            vm.ListUserReserved.Add(reserve);
            vm.RoomReserved = true;


            // Send Mail
            //SendMail("smtp.gmail.com", "shalex9@gmail.com", "399779639", UserEmail,
            //    "Бронирование номера", "Вы успешно забронировали номер! ", null);

            return View(vm);
        }

        // Send Email News    
        public static void SendMail(string smtpServer, string from, string password,
            string mailto, string caption, string message, string attachFile)
        {
            try
            {
                MailAddress fromAdress = new MailAddress("shalex9@gmail.com", "399779639");
                MailAddress toAdress = new MailAddress("shalex9@mailinator.com", "399779639");

                using (MailMessage mail = new MailMessage(fromAdress, toAdress))
                using (SmtpClient client = new SmtpClient())
                {
                    mail.Subject = caption;
                    mail.Body = message;

                    client.Host = smtpServer;
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(fromAdress.Address, password);

                    client.Send(mail);
                }
                //MailMessage mail = new MailMessage();
                //mail.From = new MailAddress(from);
                //mail.To.Add(new MailAddress(mailto));
                //mail.Subject = caption;
                //mail.Body = message;
                ////if (!string.IsNullOrEmpty(attachFile))
                ////    mail.Attachments.Add(new Attachment("wwwroot/gallery/newsImg/" + attachFile));
                //SmtpClient client = new SmtpClient();
                //client.Host = smtpServer;
                //client.Port = 587;
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("shevlex@gmail.com", password);
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.Send(mail);
                //mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        //public async Task<IActionResult> SendMessage()
        //{
        //    EmailService emailService = new EmailService();
        //    await emailService.SendEmailAsync("shalex9@maillinator.com", "Тема письма", "Тест письма: тест!");
        //    return RedirectToAction("Index");
        //}


        //AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIsFreeRoom(DateTime start, DateTime end)
        {
            DateTime now = DateTime.Now;
            var list101Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 101).ToList();
            var list102Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 102).ToList();
            var list201Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 201).ToList();
            var list202Reserved = _dbContext.HotelReservations.Where(u => u.NumberOfRoom == 202).ToList();

            var free = new FreeRoom();
            free.StartReserv = start;
            free.EndReserv = end;
            free.CountDays = (end - start).Days + 1;

            if (start >= now && end > now && start < end)
            {
                free.IsCorrectDate = true;

                bool free101 = true;
                foreach (var i in list101Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free101 = true;
                    }
                    else
                    {
                        free101 = false;
                        break;
                    }
                }
                if (free101)
                {
                    free.ListFreeRooms.Add(101);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 101));
                }

                bool free102 = true;
                foreach (var i in list102Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free102 = true;
                    }
                    else
                    {
                        free102 = false;
                        break;
                    }
                }
                if (free102)
                {
                    free.ListFreeRooms.Add(102);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 102));
                }

                bool free201 = true;
                foreach (var i in list201Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free201 = true;
                    }
                    else
                    {
                        free201 = false;
                        break;
                    }
                }
                if (free201)
                {
                    free.ListFreeRooms.Add(201);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 201));
                }

                bool free202 = true;
                foreach (var i in list202Reserved)
                {
                    if ((start < i.StartReserv && end <= i.StartReserv) || (start >= i.EndReserv && end > i.EndReserv))
                    {
                        free202 = true;
                    }
                    else
                    {
                        free202 = false;
                        break;
                    }
                }
                if (free202)
                {
                    free.ListFreeRooms.Add(202);
                    free.ListInfoFreeRooms.Add(_dbContext.HotelInformations.FirstOrDefault(u => u.NumberOfRoom == 202));
                }


                if (free.ListFreeRooms == null)
                {
                    free.RoomEngaged = true;
                }

            }
            else
            {
                free.IsCorrectDate = false;
                free.ErrorText = "Не корректные даты бронирования";
            }

            return Json(free);
        }


        //AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReserved(int id)
        {
            var reserveForDelete = _dbContext.HotelReservations.FirstOrDefault(u => u.Id == id);

            _dbContext.HotelReservations.Remove(reserveForDelete);
            _dbContext.SaveChanges();

            var deleted = "Deleted Success";
            return Json(deleted);
        }



    }
}
