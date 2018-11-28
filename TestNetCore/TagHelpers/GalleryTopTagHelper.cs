using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetCore.Models.WidgetsViewModels;
using TestNetCore.Models.Files;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestNetCore.TagHelpers
{
    [HtmlTargetElement("gallerytop")]
    public class GalleryTopTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-items")]
        public IEnumerable<string> SomeAdditionalInformation { get; set; }

        [HtmlAttributeName("gallery-img-items")]
        public IEnumerable<GalleryFileImg> GalItemImages { get; set; }
        [HtmlAttributeName("gallery-sound-items")]
        public IEnumerable<GalleryFileSound> GalItemSounds { get; set; }

        [HtmlAttributeName("upload-img-items")]
        public IEnumerable<UploadFileImg> UplItemImages { get; set; }
        [HtmlAttributeName("upload-sound-items")]
        public IEnumerable<UploadFileSound> UplItemSounds { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            string divTop = File.ReadAllText(@"templates\gallerytop.html");
            string divSidebar = File.ReadAllText(@"templates\gallerySidebar.html");
            StringBuilder divGalleryImg = new StringBuilder("");
            string templateGalleryImg = File.ReadAllText(@"templates\galleryImg.html");
            StringBuilder divGallerySound = new StringBuilder("");
            string templateGallerySound = File.ReadAllText(@"templates\gallerySound.html");
            StringBuilder divUploadImg = new StringBuilder("");
            string templateUploadImg = File.ReadAllText(@"templates\uploadImg.html");
            StringBuilder divUploadSound = new StringBuilder("");
            string templateUploadSound = File.ReadAllText(@"templates\uploadSound.html");

            string beforeImageGallery = "<div id=\"modalGalleryContent\" class=\"col-md-9\"><ul id =\"imgGalleryContainer\" class=\"galleryList images\">";
            string beforeSoundGallery = "</ul><ul id=\"soundGalleryContainer\" class=\"galleryList sounds\">";
            string beforeImageUpload = "</ul><ul id=\"imgUploadContainer\" class=\"galleryList images\">";
            string beforeSoundUpload = "</ul><ul id=\"soundUploadContainer\" class=\"galleryList sounds\">";

            string notFileImage = "<h3>Ни одного изображения не загружено." +
                " <br/> Загрузите своё изображение, или воспользуйтесь готовыми вариантами из галереи.</h3>";
            string notFileSound = "<h3>Ни одной мелодии не загружено." + 
                " <br/> Загрузите свою мелодию, или воспользуйтесь готовыми вариантами из галереи. </h3>";
            string closeTags = "</div></div></div>";


            GalleryItemImages(templateGalleryImg, divGalleryImg);
            GalleryItemSounds(templateGallerySound, divGallerySound);
            UploadItemImages(templateUploadImg, divUploadImg, notFileImage);
            UploadItemSounds(templateUploadSound, divUploadSound, notFileSound);

            string divContents= divTop + divSidebar + 
                                beforeImageGallery + divGalleryImg +
                                beforeSoundGallery + divGallerySound +
                                beforeImageUpload + divUploadImg +
                                beforeSoundUpload + divUploadSound + closeTags;

            output.Content.SetHtmlContent(divContents);
        }

        private void GalleryItemImages(string templateGalleryImg, StringBuilder divGalleryImg)
        {
            foreach (var itemImageGallery in GalItemImages)
            {
                string gallerySingleItem = templateGalleryImg.Replace("@dataFile.FileName", itemImageGallery.FileName)
                    .Replace("@dataFile.Size", itemImageGallery.Size);
                divGalleryImg.Append(gallerySingleItem);
            }
        }
        private void GalleryItemSounds(string templateGallerySound, StringBuilder divGallerySound)
        {
            foreach (var itemSoundGallery in GalItemSounds)
            {
                string gallerySingleItem = templateGallerySound.Replace("@dataFile.FileName", itemSoundGallery.FileName)
                    .Replace("@dataFile.Size", itemSoundGallery.Size);
                divGallerySound.Append(gallerySingleItem);
            }
        }
        private void UploadItemImages(string templateUploadImg, StringBuilder divUploadImg, string notFileImage)
        {
            if (UplItemImages.Count() > 0)
            {
                foreach (var itemImageUpload in UplItemImages)
                {                    
                    string gallerySingleItem = templateUploadImg.Replace("@dataFile.FileName", itemImageUpload.FileName)
                        .Replace("@dataFile.Id", itemImageUpload.Id.ToString())
                        .Replace("@dataFile.UserId", itemImageUpload.UserId.ToString())
                        .Replace("@dataFile.Size", itemImageUpload.Size);
                    divUploadImg.Append(gallerySingleItem);
                }
            }
            else
            {
                divUploadImg.Append(notFileImage);
            }
        }
        private void UploadItemSounds(string templateUploadSound, StringBuilder divUploadSound, string notFileSound)
        {
            if (UplItemSounds.Count() > 0)
            {
                foreach (var itemSoundUpload in UplItemSounds)
                {
                    string gallerySingleItem = templateUploadSound.Replace("@dataFile.FileName", itemSoundUpload.FileName)
                        .Replace("@dataFile.Id", itemSoundUpload.Id.ToString())
                        .Replace("@dataFile.UserId", itemSoundUpload.UserId.ToString())
                        .Replace("@dataFile.Size", itemSoundUpload.Size);
                    divUploadSound.Append(gallerySingleItem);
                }
            }
            else
            {
                divUploadSound.Append(notFileSound);
            }
        }
    }
}
