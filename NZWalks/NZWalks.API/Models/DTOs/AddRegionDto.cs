using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class AddRegionDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code has a minimum of 3 characters" )]
        [MaxLength(3, ErrorMessage = "Code has a maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name has a maximum of 50 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
