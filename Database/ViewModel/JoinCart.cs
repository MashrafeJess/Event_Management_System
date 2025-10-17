using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace Database.ViewModel
{
    public class JoinCart : BaseModel
    {
        [Key]
        public int CartId { get; set; }
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public int ?Price { get; set; }
        public string ?Location { get; set; }
        public DateTime ?EventDate { get; set; }
        public int OfferId { get; set; }
        public string? OfferName { get; set; }
    }
}
