

namespace CarListApp.Models
{
    public class Car : BaseEntity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }

        public string Repr()
        {
            return $"{Make} {Model}";
        }
    }

    public class CarMart : BaseEntity
    {
        public List<Car> Cars { get; set; }
    }
}
