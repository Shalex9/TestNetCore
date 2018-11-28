﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNetCore.Models.EFViewModels
{
    public class EntityFSPViewModel : BaseViewModel
    {
        public IEnumerable<UserSort> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortUserViewModel SortUserViewModel { get; set; }
    }
}
