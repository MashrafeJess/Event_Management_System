using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Database
{
    public class Standard : BaseModel
    {
        [Key]
        public int StandardId { get; set; }
        public string? StandardName { get; set; }
        public ICollection<Events> Events { get; set; }
    }
}
