using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AddOns : BaseModel
    {
        [Key]
        public int AddOnId { get; set; }
        public string? AddOnName { get; set; }
        public int Charge{ get; set; }
    }
}
