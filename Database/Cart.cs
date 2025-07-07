using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Cart : BaseModel
    {
        [Key]
        public int CartId { get; set; }
        public int StandardId { get; set; }
        public int PackageId { get; set; }
        public int OfferId { get; set; }
        public int EventId { get; set; }
        public int Price { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public List<CartAddOn>? Extra { get; set; }
    }
}
