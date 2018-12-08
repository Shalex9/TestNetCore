using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetCore.Data.TableData;

namespace TestNetCore.Models.Widgets.Dictionary
{
    public class AssociatedWords
    {
        public IQueryable<RussianDictionary> FullAndSymbols { get; set; }
        public IQueryable<RussianDictionary> FirstThreeLetters { get; set; }
        public IQueryable<RussianDictionary> FirstThreeLettersLength { get; set; }
        public IQueryable<RussianDictionary> FirstThreeConsLetters { get; set; }
        public IQueryable<RussianDictionary> FirstThreeConsLettersLength { get; set; }
        //public IQueryable<RussianDictionary> Category6 { get; set; }
        //public IQueryable<RussianDictionary> Category7 { get; set; }
    }
}
