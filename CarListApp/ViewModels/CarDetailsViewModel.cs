using CarListApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Car car;

        public CarDetailsViewModel(Car pCar)
        {
            car = pCar;
            GenerateTitle();
        }

        private void GenerateTitle()
        {
            Title = $"Car details : {Car.Make} {Car.Model}";
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if(e.PropertyName == nameof(Car)) 
            {
                GenerateTitle();
            }
        }

    }


}
