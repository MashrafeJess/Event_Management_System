using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace Database.ViewModel
{ 
    public class Package_UserInfo
    {
        [Key]
        public int PackageId { get; set; }
        public int ? EventId { get; set; }
        public string? EventName { get; set; }
        public string? SizeName { get; set; }
        public string? UserName { get; set; }
        public int? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
