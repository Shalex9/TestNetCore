using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNetCore.Models.Files
{
    [Table("SavedUserAssociations")]
    public class SavedUserAssociation
    {
        [Key()]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Word { get; set; }
        public string AllWordsAss { get; set; }
    }
}
