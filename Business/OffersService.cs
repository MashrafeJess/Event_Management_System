using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Context;

namespace Business
{
    public class OffersService
    {
        EventContext context = new EventContext();
        public Result AddOffer(Offers model)
        {
            bool exists = context.Offers.Any(x => x.OfferName == model.OfferName);
            if (exists)
            {
                return new Result(false, "Offer already exists");
            }
            context.Offers.Add(model);
            return new Result().DBcommit(context, "Offer added successfully", null, model);
        }
        public Result UpdateOffer(Offers model)
        {
            bool exists = context.Offers.Any(x=> x.OfferId == model.OfferId);
            if (!exists)
            {
                return new Result(false, "This offer doesn't exist");
            }
            context.Offers.Update(model);
            return new Result().DBcommit(context, "Offer updated successfully", null, model);
        }
      public Result List()
      {
            var offers = context.Offers.ToList();
            if (offers.Count == 0)
            {
                return new Result(false, "No offers found");
            }
            return new Result(true, "Offers found", offers);
      }
       public Result Single(int id)
        {
            var offer = context.Offers.FirstOrDefault(x => x.OfferId == id);
            if (offer == null)
            {
                return new Result(false, "Offer not found");
            }
            return new Result(true, "Offer found", offer);
        }
        public Result DeleteOffer(int id)
        {
            var offer = context.Offers.FirstOrDefault(x => x.OfferId == id);
            if (offer == null)
            {
                return new Result(false, "Offer not found");
            }
            context.Offers.Remove(offer);
            return new Result().DBcommit(context, "Offer deleted successfully", null, offer);
        }
        public Result ViewList()
        {
            var offer = context.Offer_Event_Package_User.ToList();
            if(offer.Count == 0)
            {
                return new Result(false, "No offers found");
            }
            return new Result(true, "Offers found", offer);
        }
    }
}
