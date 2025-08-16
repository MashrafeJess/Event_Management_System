using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace Database.ViewModel
{ 
    public class Package_User : BaseModel
    {
        [Key]
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
    }
}
