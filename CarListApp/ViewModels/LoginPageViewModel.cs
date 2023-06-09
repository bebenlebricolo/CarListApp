using CarListApp.Models;
using CarListApp.Helpers;
using CarListApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        private readonly CarApiService _carApiService;

        public LoginPageViewModel(CarApiService carApiService)
        {
            _carApiService = carApiService;
            Title = "Login";
        }

        [RelayCommand]
        public async Task Login()
        {
            var loginPage = Shell.Current.CurrentPage as LoginPage;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                loginPage.SetImage("confused_1.jpg");
                Password = string.Empty;
            }
            else
            {
                // Call API to attempt a login$
                var loginModel = new LoginModel(Username, Password);
                var response = await _carApiService.Login(loginModel);

                if (response is not null && !string.IsNullOrEmpty(response.AccessToken))
                {
                    // Display a welcome message
                    await SecureStorage.SetAsync("AccessToken", response.AccessToken);
                    loginPage.SetImage("happy_2.jpg");

                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(response.AccessToken);

                    await Shell.Current.DisplayAlert("Login status", _carApiService.StatusMessage, "Ok");

                    // Build a menu on the fly ... based on the user role
                    var userInfo = new UserInfo(Username, jwt.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value);
                    App.UserInfo = userInfo;

                    // Navigate to app's main page
                    MenuBuilder.BuildMenu();
                    await Shell.Current.GoToAsync(nameof(MainPage));

                }
                else
                {
                    loginPage.SetImage("sad_3.jpg");
                    await Shell.Current.DisplayAlert("Login status", _carApiService.StatusMessage, "Ok");
                    Password = "";
                }
            }

            return;
        }
    }
}
