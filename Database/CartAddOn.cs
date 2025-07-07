using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class CartAddOn 
    {
        [Key]
        public int CartAddOnId { get; set; }
        public int CartId { get; set; }
        public int AddOnId { get; set; }
        public Cart? Cart { get; set; }
        public AddOns? AddOn { get; set; }
    }
}
