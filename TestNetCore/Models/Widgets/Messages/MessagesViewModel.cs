using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Widgets.Messages
{
    public class MessagesViewModel : BaseViewModel
    {
        public string SendTo { get; set; }
        public string NameFrom { get; set; }
        public string TitleMessage { get; set; }
        public string TextMessage { get; set; }
        public string AttachFile { get; set; }
        public string PostType { get; set; }
    }
}
