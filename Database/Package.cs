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
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
    }
}
