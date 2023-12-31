﻿using CarListApp.Models;
using CarListApp.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListApp.Services;

namespace CarListApp.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        private readonly CarApiService carApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        string message = string.Empty;

        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            AddEditButtonText = createButtonText;
            this.carApiService = carApiService;
        }

        [ObservableProperty]
        bool isRefreshing = false;

        [ObservableProperty]
        string make;

        [ObservableProperty]
        string model;

        [ObservableProperty]
        string vin;

        [ObservableProperty]
        string addEditButtonText;

        [ObservableProperty]
        int carId;

        [RelayCommand]
        public async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any())
                {
                    Cars.Clear();
                }

                var cars = new List<Car>();
                if (accessType == NetworkAccess.Internet)
                {
                    cars = await carApiService.GetCars();
                }
                else
                {
                    cars = App.CarDatabaseService.GetCars();
                }

                // Reject this as a silent error
                if (cars is null)
                {
                    return;
                }

                foreach (var car in cars)
                {
                    Cars.Add(car);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await ShowAlert("Failed to retrive list of cars.");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            var car = new Car
            {
                Id = CarId,
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != 0)
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await carApiService.UpdateCar(CarId, car);
                    message = carApiService.StatusMessage;
                }
                else
                {
                    App.CarDatabaseService.UpdateCar(car);
                    message = App.CarDatabaseService.StatusMessage;
                }
            }
            else
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await carApiService.AddCar(car);
                    message = carApiService.StatusMessage;
                }
                else
                {
                    App.CarDatabaseService.AddCar(car);
                    message = App.CarDatabaseService.StatusMessage;
                }

            }
            await ShowAlert(message);
            await GetCarList();
            ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please try again");
                return;
            }

            if (accessType == NetworkAccess.Internet)
            {
                await carApiService.DeleteCar(id);
                message = carApiService.StatusMessage;
            }
            else
            {
                App.CarDatabaseService.DeleteCar(id);
                message = App.CarDatabaseService.StatusMessage;
            }
            await ShowAlert(message);
            await GetCarList();
        }

        [RelayCommand]
        void UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            Car car;
            if (accessType == NetworkAccess.Internet)
            {
                car = await carApiService.GetCar(CarId);
            }
            else
            {
                car = App.CarDatabaseService.GetCar(CarId);
            }

            Make = car.Make ?? string.Empty;
            Model = car.Model ?? string.Empty;
            Vin = car.Vin ?? string.Empty;
        }

        [RelayCommand]
        void ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}
