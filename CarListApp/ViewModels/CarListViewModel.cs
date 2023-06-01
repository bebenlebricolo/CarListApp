using CarListApp.Models;
using CarListApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CarListApp.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel()
        {
            Title = "Car list";
        }

        [RelayCommand]
        async Task GetCarListAsync()
        {
            if (IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;
                if (Cars.Any())
                {
                    Cars.Clear();
                }

                var cars = App.CarService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars : {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed while fetching cars list", "OK");
            }
            finally
            {
                IsLoading = false;
            }
            return;
        }

        [RelayCommand]
        void ClearList()
        {
            Cars.Clear();
        }

        [RelayCommand]
        async Task GetCarDetails(Car car)
        {
            if (car is null)
            {
                return;
            }
            var carDetailsViewModel = new CarDetailsViewModel(car);
            var parameters = new Dictionary<string, object>
            {
                {nameof(Car), car}
            };
            await Shell.Current.GoToAsync(nameof(CarDetailsPage), parameters);
        }

    }
}
