using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto file)
        {
            ValidateFileUpload(file);

            var imageDomain = new Image
            {
                File = file.File,
                FileExtention = Path.GetExtension(file.File.FileName),
                FileName = file.FileName,
                FileDescriptione = file.FileDescriptione,
                FileSizeInBytes = file.File.Length
            };

            if(ModelState.IsValid)
            {
                await imageRepository.Upload(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }

            if(request.File.Length > 10485760) // File larger thatn 10mb
            {
                ModelState.AddModelError("File", "Please upload a file smaller than 10mb");
            }
        }
    }
}
