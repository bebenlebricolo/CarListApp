using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            // Retrieve token from internal storage
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            
            // Try to login, send user to login page
            if (string.IsNullOrEmpty(accessToken))
            {
                await GoToLoginPage();
            }

            // Evaluate token and decide if need to be refreshed




        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async Task GoToLoMainPage()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
