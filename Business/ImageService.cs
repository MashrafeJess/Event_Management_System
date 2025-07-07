using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
namespace Business
{
    public class ImageService
    {
        EventContext context = new EventContext();
        public Result AddImage(Image model)
        {
            bool exists = context.Image.Any(x => x.ImagePath == model.ImagePath || x.ImageId == model.ImageId || x.ImageHash==model.ImageHash);
            if (exists)
            {
                return new Result(false, "Image already exists");
            }
            context.Image.Add(model);
            return new Result().DBcommit(context, "Image added successfully", null, model);
        }
        public Result UpdateImage(Image model)
        {
            bool exists = context.Image.Any(x=>x.ImageId == model.ImageId);
            if (!exists)
            {
                return new Result(false, "Image does not exists");
            }
            context.Image.Update(model);
            return new Result().DBcommit(context, "Image updated successfully", null, model);
        }
        public Result DeleteImage(Image model)
        {
            bool exists = context.Image.Any(x => x.ImagePath == model.ImagePath || x.ImageId == model.ImageId || model.ImageHash == model.ImageHash);
            if (!exists)
            {
                return new Result(false, "Image does not exists");
            }
            context.Image.Remove(model);
            return new Result().DBcommit(context, "Image deleted successfully", null, model);
        }
        public Result ImageList()
        {
            var images = context.Image.ToList();
            if (images.Count == 0)
            {
                return new Result(false, "No images found");
            }
            return new Result(true, "Images found", images);
        }
        public Result Single(int id)
        {
            var image = context.Image.FirstOrDefault(x => x.ImageId == id);
            if (image == null)
            {
                return new Result(false, "Image not found");
            }
            return new Result(true, "Image found", image);
        }
    }
}
