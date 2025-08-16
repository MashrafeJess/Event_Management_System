using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Offers : BaseModel
    {
        [Key]
        public int OfferId { get; set; }
        public int EventId { get; set; }
        public int? PackageId { get; set; }
        [Required]
        public string? OfferName { get; set; }
        [Required]
        public string? OfferDescription { get; set; }
        [Required]
        public int? OfferPrice { get; set; }
    }
}
