﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    [Table("Companies")]
    public class Company
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
