using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestNetCore.Data.TableData
{
    [Table("SettingsDonationNotifications")]
    public class SettingsDonationNotification
    {
        [Key()]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }

        // general
        public bool AnimationVisibleDon { get; set; }
        public string BgBoxDon { get; set; }
        public string BgMessageDon { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
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
        public string PathSoundDon { get; set; }
        public string NameSoundDon { get; set; }
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
        [Column(TypeName = "decimal(18, 2)")]
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
        [Column(TypeName = "decimal(18, 2)")]
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
