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
            bool x = context.Package.Any(x=>x.EventId == package.EventId && x.SizeId == package.SizeId);
            if(x)
            {
                return new Result(false, "This Package exists");
            }
            context.Package.Add(package);
            return new Result().DBcommit(context, "Package added successfully", null, package);
        }
        public Result UpdatePackage(Package package)
        {
            bool x = context.Package.Any(x => x.EventId == package.EventId && x.SizeId == package.SizeId);
            if (!x)
            {
                return new Result(false, "This Package is not found");
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
        public Result Single (int id)
        {
            var package = context.Package.FirstOrDefault(x=>x.EventId == id);
            return new Result(true, "Package found", package);  
        }
    }
}
