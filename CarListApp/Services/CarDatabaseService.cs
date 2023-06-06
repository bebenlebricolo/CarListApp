using CarListApp.Models;
using SQLite;

namespace CarListApp.Services
{
    public class CarDatabaseService
    {
        private SQLiteConnection conn;
        private string dbPath;
        public string StatusMessage = "";

        public CarDatabaseService(string dbPath)
        {
            this.dbPath = dbPath;
        }

        private void Init()
        {
            if(conn is not null)
            {
                return;
            }

            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();
                return conn.Table<Car>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data from database.";
            }

            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public int DeleteCar(int id)
        {
            int deletedCarsCount = 0;
            try
            {
                Init();
                deletedCarsCount = conn.Table<Car>().Delete(q => q.Id == id);
                StatusMessage = "Successfully deleted car from db.";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data.";
            }

            return deletedCarsCount;
        }

        public void AddCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Invalid Car Record");

                var result = conn.Insert(car);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to Insert data.";
            }
        }

        public void UpdateCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Invalid Car Record");

                var result = conn.Update(car);
                StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to Update data.";
            }
        }
    }
}
