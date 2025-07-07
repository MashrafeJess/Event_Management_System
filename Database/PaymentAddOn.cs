using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class PaymentAddOn
    {
        public int PaymentAddOnId { get; set; }
        public string? PaymentId { get; set; }
        public int AddOnId { get; set; }
        public Payment? Payment { get; set; } // Navigation property to Payment
        public AddOns? AddOn { get; set; } // Navigation property to AddOns
    }
}
