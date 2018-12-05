using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.Files
{
    public class DictionaryViewModel : BaseViewModel
    {
        public string CurrentWord { get; set; }
        public int CountWord { get; set; }
        public List<SavedUserAssociation> ListAssociation { get; set; }

        public DictionaryViewModel()
        {
            ListAssociation = new List<SavedUserAssociation>();
        }
    }
}
