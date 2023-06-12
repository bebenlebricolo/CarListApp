namespace CarListApp.Models
{
    public class LoginModel
    {
        public LoginModel(string username, string password, uint? duration)
        {
            Username = username;
            Password = password;
            TokenLifespanRequestMinutes = duration ?? 1;
        }

        public string Username { get; set; } = string.Empty;    

        public string Password { get; set; } = string.Empty;

        public uint TokenLifespanRequestMinutes { get; set; } = 1;
    }
}