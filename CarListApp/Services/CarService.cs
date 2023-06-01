using CarListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Services
{
    public class CarService
    {
        public List<Car> GetCars()
        {
            return new List<Car>()
            {
                new Car { Id = 0, Make = "Renault" , Model = "4L", Vin = "123"},
                new Car { Id = 1, Make = "Renault" , Model = "Espace", Vin = "123"},
                new Car { Id = 2, Make = "Renault" , Model = "Clio", Vin = "123"},
                new Car { Id = 3, Make = "Citroen" , Model = "2 Chevaux", Vin = "123"},
                new Car { Id = 4, Make = "Citroen" , Model = "DS", Vin = "123"},
                new Car { Id = 5, Make = "Peugeot" , Model = "205", Vin = "123"},
            };
        }
    }
}
