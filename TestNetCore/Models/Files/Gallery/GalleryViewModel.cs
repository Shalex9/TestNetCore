using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Files.Gallery
{
    public class GalleryViewModel : BaseViewModel
    {
        public string PostSaveSettings { get; set; }

        // general
        public string BgBox { get; set; }
        public string BgMessage { get; set; }
        public decimal BgMessageOpacity { get; set; }
        public int AnimationDuration { get; set; }
        public int AnimationDelay { get; set; }
        public string LayoutMessage { get; set; }


        //image
        public string NameImage { get; set; }
        public string PathImage { get; set; }
        public string StartEffectAnimation { get; set; }
        public string FinishEffectAnimation { get; set; }
        public int CaliberImage { get; set; }

        //sound
        public bool VoiceVisible { get; set; }
        public string NameSound { get; set; }
        public string NameVoice { get; set; }
        public string PathSound { get; set; }
        public string PathVoice { get; set; }
        public int SoundVolume { get; set; }

        // for Gallery
        public string SelectGalleryBox { get; set; }
        public string SelectImageSource { get; set; }
        public string SelectSoundSource { get; set; }
        public bool GalleryVisible { get; set; }
        public int? IdForDeletionImage { get; set; }
        public int? IdForDeletionSound { get; set; }
        public string TypeUploadFile { get; set; }
        public string NewNameImage { get; set; }
        public string NewNameSound { get; set; }
        public int ImageGalleryCount { get; set; }
        public int SoundGalleryCount { get; set; }
        public int ImageUploadCount { get; set; }
        public int SoundUploadCount { get; set; }

        public string PostUpload { get; set; }
        public string ImagePath { get; set; }
        public string SoundPath { get; set; }

        public List<GalleryFileImg> GalleryItemImgs { get; set; }
        public List<GalleryFileSound> GalleryItemSounds { get; set; }
        public List<UploadFileImg> UploadItemImgs { get; set; }
        public List<UploadFileSound> UploadItemSounds { get; set; }

        public GalleryViewModel()
        {
            GalleryItemImgs = new List<GalleryFileImg>();
            GalleryItemSounds = new List<GalleryFileSound>();
            UploadItemImgs = new List<UploadFileImg>();
            UploadItemSounds = new List<UploadFileSound>();
        }

    }
}
