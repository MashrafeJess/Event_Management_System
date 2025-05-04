using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Package : BaseModel
    {
        [Key]
        public int EventId {get; set;}
        public int SizeId { get; set; }
        public decimal PriceValue { get; set; }
    }
}
