using CarListApp.Controls;
using Microsoft.Maui.Controls;

namespace CarListApp.Helpers
{
    public static class MenuBuilder
    {
        public static void BuildMenu()
        {
            Shell.Current.FlyoutHeader = new FlyoutHeader();
            var role = App.UserInfo.Role;

            if(role.Equals("Administrator"))
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
                         },
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "Admin page 2",
                             ContentTemplate = new DataTemplate(typeof(MainPage))
                         },
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "Admin page 3",
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
                         },
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "User page 2",
                             ContentTemplate = new DataTemplate(typeof(MainPage))
                         },
                         new ShellContent
                         {
                             Icon = "dotnet_bot.svg",
                             Title = "User page 3",
                             ContentTemplate = new DataTemplate(typeof(MainPage))
                         }
                    }
                };

                if(!Shell.Current.Items.Contains(flyoutItem))
                {
                    Shell.Current.Items.Add(flyoutItem);
                }
            }

        }
    }
}
