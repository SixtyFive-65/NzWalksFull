using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped] // should be excluded from DB mapping, migration will ignore this
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescriptione { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
