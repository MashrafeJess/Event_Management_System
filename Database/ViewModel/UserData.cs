using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace Database.ViewModel
{
    public class UserData
    {
        [Key]
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
