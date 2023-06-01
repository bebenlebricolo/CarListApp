using CarListApp.Models;
using CarListApp.ViewModels;
namespace CarListApp.Views;

public partial class CarDetailsPage : ContentPage, IQueryAttributable
{
    public CarDetailsViewModel CarDetailsViewModel { get; private set; }

	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = carDetailsViewModel;
        CarDetailsViewModel = carDetailsViewModel;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Car car = query[nameof(Car)] as Car;
        CarDetailsViewModel.Car = car;
        Title = CarDetailsViewModel.Title;
    }
}