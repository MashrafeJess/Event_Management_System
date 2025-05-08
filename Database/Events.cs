using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Events : BaseModel
    {
        [Key]
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? EventPic { get; set; }
    }
}
