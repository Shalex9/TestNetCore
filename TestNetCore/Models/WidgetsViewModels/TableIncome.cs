using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestNetCore.Models
{
    [Table("TableIncomes")]
    public class TableIncome
    {
        [Key()]
        public int Id { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public string NameDonator { get; set; }
        public string MessageText { get; set; }
        public string PaymentSystem { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Income { get; set; }
        public string DonatCurrency { get; set; }
        public string DonatorId { get; set; }
        public string VoiceName { get; set; }
        public string UserId { get; set; }
    }
}
