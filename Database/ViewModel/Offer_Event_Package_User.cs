using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ViewModel
{
    public class Offer_Event_Package_User : BaseModel
    {
        [Key]
        public int OfferId { get; set; }
        public string? EventName { get; set; }
        public string? PackageName { get; set; }
        public string? OfferName { get; set; }
        public string? OfferDescription { get; set; }
        public int? OfferPrice { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
    }
}
