using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database;
using Microsoft.EntityFrameworkCore;
namespace Business
{
    public class ImageService
    {
        EventContext context = new EventContext();
        public Result AddImages(List<Image> images)
        {
            if (images == null || images.Count == 0)
                return new Result(false, "No images provided");

            var imagePaths = images.Select(i => i.ImagePath).ToList();
            var imageHashes = images.Select(i => i.ImageHash).Where(h => !string.IsNullOrEmpty(h)).ToList();

            bool anyExists = context.Image.Any(x => imagePaths.Contains(x.ImagePath)
                                                || (x.ImageHash != null && imageHashes.Contains(x.ImageHash)));

            if (anyExists)
            {
                return new Result(false, "One or more images already exist");
            }

            context.Image.AddRange(images);

            return new Result().DBcommit(context, $"{images.Count} images added successfully", null, images);
        }
        public Result UpdateImage(Image model)
        {
            bool exists = context.Image.Any(x => x.ImageId == model.ImageId);
            if (!exists)
            {
                return new Result(false, "Image does not exists");
            }
            context.Image.Update(model);
            return new Result().DBcommit(context, "Image updated successfully", null, model);
        }
        public async Task<Result> RemoveImage(string imagePath, int imageId)
        {
            // Find the image in the database
            var image = await context.Image.FirstOrDefaultAsync(x => x.ImagePath == imagePath && x.ImageId == imageId);
            if (image == null)
            {
                return new Result(false, "Image not found.");
            }

            try
            {
                // Delete the image from the database
                context.Image.Remove(image);

                // Delete the image file from the server
                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImagePath.TrimStart('/'));
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);  // Delete the image file
                }
                else
                {
                    return new Result(false, "Image file not found on the server.");
                }

                // Commit changes to the database
                await context.SaveChangesAsync();

                return new Result(true, "Image removed successfully.");
            }
            catch (Exception ex)
            {
                // Catch any errors and return a detailed message
                return new Result(false, $"Error while removing image: {ex.Message}");
            }
        }

        public async Task<Result> ReplaceImage(int imageId, string newFilePath, string hash, string updatedBy)
        {
            Image existingImage = null;
            try
            {
                // Find the image in the database
                existingImage = context.Image.FirstOrDefault(x => x.ImageId == imageId);
                if (existingImage == null)
                {
                    return new Result(false, "Image not found");
                }

                // Delete the old image file from the server
                var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingImage.ImagePath.TrimStart('/'));
                if (File.Exists(oldFullPath))
                {
                    try
                    {
                        File.Delete(oldFullPath);  // Delete the old image file
                    }
                    catch (Exception ex)
                    {
                        return new Result(false, $"Failed to delete old image: {ex.Message}");
                    }
                }
                else
                {
                    return new Result(false, "Old image file not found on the server.");
                }

                // Update the image details in the database
                existingImage.ImagePath = newFilePath;  // Update the image path to the new file
                existingImage.ImageHash = hash;         // Update the image hash
                existingImage.UpdatedBy = updatedBy;
                existingImage.UpdatedDate = DateTime.UtcNow; // Update the timestamp

                // Save the changes to the database
                context.Image.Update(existingImage);
                await context.SaveChangesAsync();

                return new Result(true, "Image replaced successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, $"Error replacing image: {ex.Message}");
            }
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
        public Result EventImages(int id)
        {
            List<Image> images = new List<Image>();
            images = context.Image.Where(x => x.EventId == id).ToList();
            if (images.Count == 0)
            {
                return new Result(false, "No images found for this event");
            }
            return new Result(true, "Images found", images);
        }
    }
}
