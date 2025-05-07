using System.ComponentModel.DataAnnotations;
namespace Database
{
    public class PrevOrders : BaseModel
    {
        [Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string? EventName { get; set; }
        public string? SizeName { get; set; }
        public int? Price { get; set; }
        public string? Location { get; set; }
        public DateTime EventDate { get; set; }
    }
}
