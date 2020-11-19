using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTime.Models
{
    public class BillingRateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Column(TypeName = "decimal(3,2)")]
        public Decimal BillingRate { get; set; }
        public DateTime Date { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
