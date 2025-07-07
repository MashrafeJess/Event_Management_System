using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace Database.ViewModel
{
    public class PaymentAddOnView
    {
        [Key]
        public int PaymentAddOnId { get; set; }
        public string? PaymentId { get; set; } 
        public int AddOnId { get; set; }
    }
}
