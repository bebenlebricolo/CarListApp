using System.ComponentModel.DataAnnotations;

namespace CarListApi.Models
{
    public class Car
    {
        public int Id { get; set; } = 0;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Vin { get; set; } = string.Empty;

        public void CopyFrom(Car other)
        {
            Id = other.Id;
            Make = other.Make;      
            Model = other.Model;    
            Vin = other.Vin;    
        }
    }
}