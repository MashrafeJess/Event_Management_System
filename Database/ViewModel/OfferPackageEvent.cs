using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ViewModel
{
    public class OfferPackageEvent
    {
        [Key]
        public int OfferId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public int OfferPrice { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
    }
}
