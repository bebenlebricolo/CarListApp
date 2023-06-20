using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarListApp.Helpers;
using CarListApp.Models;
using CarListApp.Views;

namespace CarListApp.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        public LoadingPageViewModel()
        {
        }

        public async Task CheckUserLoginDetails()
        {
            // Retrieve token from internal storage
            var accessToken = await SecureStorage.GetAsync("AccessToken");

            // Try to login, send user to login page
            if (string.IsNullOrEmpty(accessToken))
            {
                await GoToLoginPage();
                // Need the return statement to prevent .Net Maui to continue executing syncronous code (like the jwt stuff) and breaking with an exception
                // So returning now forces to pop this method from the stack and to do proper cleanup
                return;
            }

            // Evaluate token and decide if need to be refreshed
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            if (jwt.ValidTo < DateTime.UtcNow)
            {
                SecureStorage.Remove("AccessToken");
                await GoToLoginPage();
            }
            else
            {
                // Build a menu on the fly ... based on the user role
                var userInfo = new UserInfo(jwt.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Email))?.Value,
                                            jwt.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value,
                                            App.UserInfo != null ? App.UserInfo.Password : string.Empty);
                App.UserInfo = userInfo;


                MenuBuilder.BuildMenu();
                await GoToMainPage();
            }
        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
