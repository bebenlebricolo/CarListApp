using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        public LoginPageViewModel()
        {
            Title = "Login";
        }

        [RelayCommand]
        public Task Login()
        {
            var loginPage = Shell.Current.CurrentPage as LoginPage;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                loginPage.SetImage("confused_1.jpg");
                Password = string.Empty;
            }
            else
            {
                // Call API to attempt a login
                var loginSuccessful = true;
                if(loginSuccessful)
                {
                    // Display a welcome message
                    loginPage.SetImage("happy_2.jpg");

                    // Build a menu on the fly ... based on the user role

                    // Navigate to app's main page

                }
            }

            return Task.CompletedTask; 
        }
    }
}
