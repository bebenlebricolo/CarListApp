using CarListApp.ViewModels;

namespace CarListApp.Views;

public class LogoutPage : ContentPage
{
	public LogoutPage(LogoutPageViewModel logoutPageViewModel)
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Logging out."
				}
			}
		};

		BindingContext = logoutPageViewModel;
	}
}