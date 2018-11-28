using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models
{
    [Table("ClaimsDataUsers")]
    public class ClaimsDataUser
    {
        [Key()]
        public int Id { get; set; }
        public string UserProviderKey { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserLinkGooglePlus { get; set; }
        public string UserLinkPicasa { get; set; }
        public string UserAvatar { get; set; }
    }
}
