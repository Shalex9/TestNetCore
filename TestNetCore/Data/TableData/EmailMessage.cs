using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Data.TableData
{
    [Table("EmailMessages")]
    public class EmailMessage
    {
        [Key()]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendTo { get; set; }
        public string NameFrom { get; set; }
        public string TitleMessage { get; set; }
        public string TextMessage { get; set; }
        public string AttachFile { get; set; }
        public string VoiceName { get; set; }
        public DateTime DateMessage { get; set; }
    }
}
