using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.Files.Crud
{
    public class CrudViewModel : BaseViewModel
    {
        public string PostType { get; set; }
        public string TypeUploadFile { get; set; }
        public int CountFiles { get; set; }
        public string FileName1 { get; set; }
        public string FileSize1 { get; set; }
        public string FileName2 { get; set; }
        public string FileSize2 { get; set; }
        public DateTime DateChange { get; set; }
    }
}
