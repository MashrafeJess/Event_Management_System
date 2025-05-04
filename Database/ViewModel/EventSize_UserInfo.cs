using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Database.ViewModel
{
    public class EventSize_UserInfo
    {
        [Key]
        public long SizeId { get; set; }
        public string  ?SizeName { get; set; }
        public DateTime ?CreatedDate { get; set; }
        public string? UserName { get; set; }
    }
}
