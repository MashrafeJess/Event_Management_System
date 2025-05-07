using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class MaxOrderLimit : BaseModel
    {
        [Key]
        public int MaxOrderId { get; set; }
        public int? MaxOrder{ get; set; }

    }
}
