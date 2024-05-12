using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class LoginResponseDto
    {
        public string JWTToken { get; set; }
    }
}
