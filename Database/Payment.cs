using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.ViewModel;
namespace Database
{
    public class Payment : BaseModel
    {
        [Key]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();
        public int StandardId { get; set; }
        public int PackageId { get; set; }
        public int OfferId { get; set; }
        public int Bill { get; set; }
        public DateTime? EventDate { get; set; }
        public string? Location { get; set; }
        public int PaymentMethodId { get; set; }
        public int TotalAddOns { get; set; }
        public int OrderStatusId { get; set; } = 1;
        public bool IsNotified { get; set; } = false;
        public ICollection<PaymentAddOn>?AddOnIds { get; set; }
    }
}
