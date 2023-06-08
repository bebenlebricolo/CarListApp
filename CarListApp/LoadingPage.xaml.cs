using CarListApp.ViewModels;

namespace CarListApp;

public partial class LoadingPage : ContentPage
{

	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext = loadingPageViewModel;
	}
}