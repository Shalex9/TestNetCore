using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestNetCore.Data.TableData
{
    [Table("AllUserWidgets")]
    public class AllUserWidget
    {
        [Key()]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int WidgetId { get; set; }
        public string WidgetUrl { get; set; }
        public bool RunTestMessage { get; set; }
    }
}
