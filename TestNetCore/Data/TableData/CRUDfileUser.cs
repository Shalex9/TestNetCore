using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNetCore.Data.TableData
{
    [Table("CRUDfileUsers")]
    public class CRUDfileUser
    {
        [Key()]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName1 { get; set; }
        public string FilePath1 { get; set; }
        public string FileSize1 { get; set; }
        public string FileName2 { get; set; }
        public string FilePath2 { get; set; }
        public string FileSize2 { get; set; }
    }
}
