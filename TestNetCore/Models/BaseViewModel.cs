using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models
{
    public class BaseViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Currency { get; set; }

        public int CountNewQuestion { get; set; }
        public int CountNewAnswer { get; set; }
        public bool HasAnswer { get; set; }
        public string UserName { get; set; }
        public bool GoogleAvatar { get; set; }
        public string AvatarPath { get; set; }
        public bool IsAdmin { get; set; }
        public bool VerifyChanges { get; set; }
        public string ChosenLanguage { get; set; }
    }
}
