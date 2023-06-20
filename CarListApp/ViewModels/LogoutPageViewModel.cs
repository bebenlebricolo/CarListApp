using CarListApp.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.ViewModels
{
    public partial class LogoutPageViewModel : BaseViewModel
    {
        public LogoutPageViewModel()
        {
            Logout();
        }

        [RelayCommand]
        public async void Logout()
        {
            SecureStorage.Remove("AccessToken");
            App.UserInfo = null;
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
