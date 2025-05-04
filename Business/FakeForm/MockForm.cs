using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business.FakeForm
{
    public class MockForm : BaseModel
    {
        [Required, MaxLength(50)]
        public string? UserName { get; set; }
        [Required, MaxLength(50)]
        public string? Email { get; set; }
        [Required, MinLength(8)]
        public string? Password { get; set; }
        public string? PhoneNum { get; set; }
        public int Role { get; set; }
    }
}
