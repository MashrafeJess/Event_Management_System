using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Top10Images
    {
        [Key]
        public int Top10ImageId { get; set; }
        public int ImageId { get; set; }    
        public string? ImagePath { get; set; }
    }
}
