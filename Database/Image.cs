using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Database;

namespace Database
{
    public class Image : BaseModel
    {
        [Key]
        public int ImageId { get; set; } 
        public int? EventId { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageHash { get; set; }
        public Events? Event { get; set; }
    }
}
