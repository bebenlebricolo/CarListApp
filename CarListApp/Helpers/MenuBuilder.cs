using CarListApp.Controls;
using CarListApp.Views;
using Microsoft.Maui.Controls;

namespace CarListApp.Helpers
{
    public static class MenuBuilder
    {
        public static void BuildMenu()
        {
            // On Windows platforms, clearing the items now break the UI.. (crashes the app because of a null reference exception)
            // But it does work great on Android as-is..
#if !WINDOWS
            Shell.Current.Items.Clear();
#endif
            Shell.Current.FlyoutHeader = new FlyoutHeader();
            var role = App.UserInfo.Role;

            if (role.Equals("Administrator"))
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Car management",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "Admin page 1",
                             ContentTemplate = new DataTemplate(typeof(MainPage))
                         }
                    }
                };

                if (!Shell.Current.Items.Contains(flyoutItem))
                {
                    Shell.Current.Items.Add(flyoutItem);
                }
            }
            else
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Car management",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "User page 1",
                             ContentTemplate = new DataTemplate(typeof(MainPage))
                         }
                    }
                };

                if (!Shell.Current.Items.Contains(flyoutItem))
                {
                    Shell.Current.Items.Add(flyoutItem);
                }
            }

            var logoutItem = new FlyoutItem()
            {
                Title = "Logout",
                Route = nameof(LogoutPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Items =
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "Logout",
                        ContentTemplate = new DataTemplate(typeof(LogoutPage))
                    }
                }
            };

            Shell.Current.Items.Add(logoutItem);
        }
    }
}

