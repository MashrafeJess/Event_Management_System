using System.ComponentModel.DataAnnotations;
namespace Database.ViewModel
{
    public class OrderList
    {
        [Key]
        public string OrderId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EventName { get; set; }
        public string? SizeName { get; set; }
        public DateTime EventDate { get; set; }
        public string? Location { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
