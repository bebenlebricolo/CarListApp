using CarListApp.ViewModels;

namespace CarListApp;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
		BindingContext = loginPageViewModel;
	}

	public void SetImage(string imageUrl)
	{
        TurtleImageWidget.Source = ImageSource.FromFile(imageUrl);
    }
}