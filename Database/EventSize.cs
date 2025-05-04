using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class EventSize : BaseModel
    {
        [Key]
        public int SizeId { get; set; }
        [Required]
        public string? SizeName { get; set; }
    }
}
