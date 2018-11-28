using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.Files
{
    [Table("SettingsGalleryPages")]
    public class SettingsPageGallery
    {
        [Key()]
        public int Id { get; set; }
        public string UserId { get; set; }

        // general
        public string BgBox { get; set; }
        public string BgMessage { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
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
        public string PathSound { get; set; }
        public string NameSound { get; set; }
        public int SoundVolume { get; set; }
    }
}
