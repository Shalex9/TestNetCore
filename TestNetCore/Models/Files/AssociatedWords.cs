using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.Files
{
    public class AssociatedWords
    {
        public IQueryable<RussianDictionary> Category1 { get; set; }
        public IQueryable<RussianDictionary> Category2 { get; set; }
        public IQueryable<RussianDictionary> Category3 { get; set; }
        public IQueryable<RussianDictionary> Category4 { get; set; }
        public IQueryable<RussianDictionary> Category5 { get; set; }
    }
}
