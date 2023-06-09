using CarListApp.ViewModels;

namespace CarListApp;

public partial class LoadingPage : ContentPage
{
	private readonly LoadingPageViewModel viewModel;

	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext = loadingPageViewModel;
		viewModel = loadingPageViewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await viewModel.CheckUserLoginDetails();
    }
}