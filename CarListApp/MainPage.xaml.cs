
using CarListApp.ViewModels;

namespace CarListApp;

public partial class MainPage : ContentPage
{
    private readonly CarListViewModel carListViewModel;

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
        this.carListViewModel = carListViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await carListViewModel.GetCarList();
    }
}

