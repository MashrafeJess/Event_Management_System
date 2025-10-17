using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;

namespace Business
{
    public class PackageService
    {
        EventContext context = new EventContext();
        public Result AddPackage(Package package)
        {
            bool x = context.Package.Any(x => x.PackageName == package.PackageName);
            if (x)
            {
                return new Result(false, "This Package exists");
            }
            context.Package.Add(package);
            return new Result().DBcommit(context, "Package added successfully", null, package);
        }
        public Result UpdatePackage(Package package)
        {
            bool x = context.Package.Any(x => x.PackageId == package.PackageId);
            if (!x)
            {
                return new Result(false, "This Package is not found");
            }
            bool y = context.Package.Any(x => x.PackageName == package.PackageName && x.PackageId != package.PackageId);
            if (y)
            {
                return new Result(false, "This Package of certain name already exists");
            }
            context.Package.Update(package);
            return new Result().DBcommit(context, "Package updated successfully", null, package);
        }
        public Result List()
        {
            var packages = context.Package.ToList();
            if (packages.Count == 0)
            {
                return new Result(false, "No packages found");
            }
            return new Result(true, "Packages found", packages);
        }
        public Result Single(int id)
        {
            var package = context.Package.FirstOrDefault(x => x.PackageId == id);
            return new Result(true, "Package found", package);
        }
        public Result PackageNewList()
        {
            var packages = context.Package_User.ToList();
            return new Result(true, "Packages found", packages);
        }
        public Result PackageInfoList(int Id)
        {
            var package = context.Package_User.FirstOrDefault(x => x.PackageId == Id);
            return new Result(true, "Package found", package);
        }
        public Result EventPackages(int Id)
        {
            var EventOffer = context.OfferPackageEvent
            .Where(x => x.EventId == Id)
             .AsEnumerable()   // move to memory (LINQ to Objects)
             .DistinctBy(x => x.PackageId)
                .ToList();

            if (EventOffer.Count == 0)
            {
                return new Result(false, "No offers found for this event");
            }
            return new Result(true, "Offers found", EventOffer);
        }
        public Result AllPackageNamesOnly()
        {
            var model = context.Package
                .Select(x => new Package
                {
                    PackageId = x.PackageId,
                    PackageName = x.PackageName
                })
                .ToList();

            if (!model.Any())
            {
                return new Result(false, "No packages found", null);
            }

            return new Result(true, "Successfully retrieved all package names", model);
        }
    }

}
