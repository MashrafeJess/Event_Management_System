using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Standard : BaseModel
    {
        [Key]
        public int StandardId { get; set; }
        public string? StandardName { get; set; }
    }
}
