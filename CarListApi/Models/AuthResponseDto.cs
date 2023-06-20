namespace CarListApi.Models
{
    public class AuthResponseDto
    {
        public string Username { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        
        public string AccessToken { get; set; } = string.Empty;

        public string ValidFrom { get; set; } = string.Empty;
        
        public string ValidUntil{ get; set; } = string.Empty;
    }
}