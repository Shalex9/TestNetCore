﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestNetCore.Data.TableData
{
    [Table("UploadFilesImgs")]
    public class UploadFileImg
    {
        [Key()]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
        public string Size { get; set; }
    }
}
