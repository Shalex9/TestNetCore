using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Widgets.Dictionary
{
    public class DictionaryViewModel : BaseViewModel
    {
        public string CurrentWord { get; set; }
        public int CountWord { get; set; }

        public bool FullAndSymbols { get; set; }
        public bool FirstThreeLetters { get; set; }
        public bool FirstThreeLettersLength { get; set; }
        public bool FirstThreeConsLetters { get; set; }
        public bool FirstThreeConsLettersLength { get; set; }

        public List<SavedUserAssociation> ListAssociation { get; set; }

        public DictionaryViewModel()
        {
            ListAssociation = new List<SavedUserAssociation>();
        }
    }
}
