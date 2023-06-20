using System.ComponentModel.DataAnnotations;

namespace CarListApi.Models
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = "";
        
        [Required]
        public string Password { get; set; } = "";

        public uint TokenLifespanRequestMinutes { get; set; } = 1;
    }
}