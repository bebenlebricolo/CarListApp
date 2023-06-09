namespace CarListApp.Models
{
    public class LoginModel
    {
        public LoginModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; } = string.Empty;    

        public string Password { get; set; } = string.Empty;
    }
}