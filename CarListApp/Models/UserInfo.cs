namespace CarListApp.Models
{
    public class UserInfo
    {
        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserInfo(string username, string role, string password)
        {
            Username = username;
            Role = role;
            Password = password;
        }
    }
}
