using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class UserInfo : BaseModel
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ?UserName { get; set; }
        [Required]
        public string ?Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        public string ?PhoneNumber { get; set; }    

        public int Role { get; set; } 
        public bool IsActive { get; set; }
    }
}
