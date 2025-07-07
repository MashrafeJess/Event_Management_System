using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class PaymentMethod : BaseModel
    {
        [Key]
        public int PaymentMethodId { get; set; }
        [Required]
        public string? PaymentMethodName { get; set; }
        public bool IsPayable { get; set; }
    }
}
