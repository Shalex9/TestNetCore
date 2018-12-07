using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.WidgetsViewModels
{
    public class DonatViewModel : WidgetsBaseViewModel
    {
        public string UserGuid { get; set; }
        public string LinkDonation { get; set; }

        public bool ClickRunTestAnimation { get; set; }
        public bool RunTestMessage { get; set; }

        // for Gallery
        public string SelectGalleryBox { get; set; }
        public string SelectImageSource { get; set; }
        public string SelectSoundSource { get; set; }
        public bool GalleryVisible { get; set; }
        public int? IdForDeletionImage { get; set; }
        public int? IdForDeletionSound { get; set; }
        public string TypeUploadFile { get; set; }
        public string NewNameImageDon { get; set; }
        public string NewNameSoundDon { get; set; }
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

        public DonatViewModel()
        {
            GalleryItemImgs = new List<GalleryFileImg>();
            GalleryItemSounds = new List<GalleryFileSound>();
            UploadItemImgs = new List<UploadFileImg>();
            UploadItemSounds = new List<UploadFileSound>();
        }

        // in table SettingMessageDONATOR
        // general
        public bool AnimationVisibleDon { get; set; }
        public string BgBoxDon { get; set; }
        public string BgMessageDon { get; set; }
        public decimal BgMessageOpacityDon { get; set; }
        public int AnimationDurationDon { get; set; }
        public int AnimationDelayDon { get; set; }
        public string LayoutMessageDon { get; set; }
        public string TokenDon { get; set; }

        //image
        public bool AnimationImageVisibleDon { get; set; }
        public string NameImageDon { get; set; }
        public string PathImageDon { get; set; }
        public string StartEffectAnimationDon { get; set; }
        public string FinishEffectAnimationDon { get; set; }
        public int CaliberImageDon { get; set; }

        //sound
        public bool SoundVisibleDon { get; set; }
        public bool VoiceVisibleDon { get; set; }
        public string NameSoundDon { get; set; }
        public string NameVoiceDon { get; set; }
        public string PathSoundDon { get; set; }
        public string PathVoiceDon { get; set; }
        public int SoundVolumeDon { get; set; }

        //text
        public int MinAmountDon { get; set; }
        public int TextDelayDon { get; set; }
        public string TemplateTextDon { get; set; }
        public string StartEffectAnimationTextDon { get; set; }
        public string FinishEffectAnimationTextDon { get; set; }

        //title
        public string FontFamilyTitleDon { get; set; }
        public string FontColorTitleDon { get; set; }
        public decimal FontColorTitleOpacityDon { get; set; }
        public string FontHighlightColorTitleDon { get; set; }
        public string FontSizeTitleDon { get; set; }
        public string FontWeightTitleDon { get; set; }
        public string FontItalicTitleDon { get; set; }
        public string FontUnderlineTitleDon { get; set; }
        public string LetterSpacingTitleDon { get; set; }
        public string WordSpacingTitleDon { get; set; }
        public string ShadowTitleDon { get; set; }
        public string FontAnimationTitleDon { get; set; }
        //textMessage
        public string FontFamilyTextDon { get; set; }
        public string FontColorTextDon { get; set; }
        public decimal FontColorTextOpacityDon { get; set; }
        public string FontSizeTextDon { get; set; }
        public string FontWeightTextDon { get; set; }
        public string FontItalicTextDon { get; set; }
        public string FontUnderlineTextDon { get; set; }
        public string LetterSpacingTextDon { get; set; }
        public string WordSpacingTextDon { get; set; }
        public string ShadowTextDon { get; set; }
        public string FontAnimationTextDon { get; set; }

    }


    public class ExecuteDonationAlertModel
    {
        public ExecuteAnimationModel SettingsDonat { get; set; }
        public bool Run { get; set; }
    }


    public class ExecuteAnimationModel
    {
        public string UrlRead { get; set; }
        public string Guid { get; set; }

        public bool RunAnimationDon { get; set; }

        public bool AnimationVisibleDon { get; set; }
        public string BgBoxDon { get; set; }
        public string BgMessageDon { get; set; }
        public decimal BgMessageOpacityDon { get; set; }
        public int AnimationDurationDon { get; set; }
        public int AnimationDelayDon { get; set; }
        public string LayoutMessageDon { get; set; }

        //image
        public bool AnimationImageVisibleDon { get; set; }
        public string NameImageDon { get; set; }
        public string PathImageDon { get; set; }
        public string StartEffectAnimationDon { get; set; }
        public string FinishEffectAnimationDon { get; set; }
        public int CaliberImageDon { get; set; }

        //sound
        public bool SoundVisibleDon { get; set; }
        public string NameSoundDon { get; set; }
        public string PathSoundDon { get; set; }
        public int SoundVolumeDon { get; set; }

        //text
        public int MinAmountDon { get; set; }
        public int TextDelayDon { get; set; }
        public string TemplateTextDon { get; set; }
        public string StartEffectAnimationTextDon { get; set; }
        public string FinishEffectAnimationTextDon { get; set; }

        //title
        public string FontFamilyTitleDon { get; set; }
        public string FontColorTitleDon { get; set; }
        public decimal FontColorTitleOpacityDon { get; set; }
        public string FontHighlightColorTitleDon { get; set; }
        public string FontSizeTitleDon { get; set; }
        public string FontWeightTitleDon { get; set; }
        public string FontItalicTitleDon { get; set; }
        public string FontUnderlineTitleDon { get; set; }
        public string LetterSpacingTitleDon { get; set; }
        public string WordSpacingTitleDon { get; set; }
        public string ShadowTitleDon { get; set; }
        public string FontAnimationTitleDon { get; set; }
        //textMessage
        public string FontFamilyTextDon { get; set; }
        public string FontColorTextDon { get; set; }
        public decimal FontColorTextOpacityDon { get; set; }
        public string FontSizeTextDon { get; set; }
        public string FontWeightTextDon { get; set; }
        public string FontItalicTextDon { get; set; }
        public string FontUnderlineTextDon { get; set; }
        public string LetterSpacingTextDon { get; set; }
        public string WordSpacingTextDon { get; set; }
        public string ShadowTextDon { get; set; }
        public string FontAnimationTextDon { get; set; }
    }

}
