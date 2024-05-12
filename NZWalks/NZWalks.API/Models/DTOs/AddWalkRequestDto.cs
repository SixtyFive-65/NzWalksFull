using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name has a maximum of 20 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name has a maximum of 50 characters")]
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
