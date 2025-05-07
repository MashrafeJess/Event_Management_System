using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Cart : BaseModel
    {
        [Key]
        public int CartId { get; set; }
        public int PackageId { get; set; }
        public string? EventName { get; set; }
        public string? SizeName { get; set; }
        public int Price { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
