using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nZWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext nZWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            //var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Image", image.FileName, image.FileExtention);  //breaks because it adds an extra space

            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");// name and extention go together


            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);  //Upload image to local Images folder

            // add HttpContextAccessor on program.cs

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";  ///Path to upload to table so we can access running application 

            image.FilePath = urlFilePath; // path saved to database so we can access from running application

            await nZWalksDbContext.Images.AddAsync(image);  // save image to database
            await nZWalksDbContext.SaveChangesAsync();


            //Inorder for clients to be able to retreive static files, we need to add configs to the program.cs......by default asp.net doesn't allow loading of static files

            // app.UseStaticFiles

            return image;
        }
    }
}
