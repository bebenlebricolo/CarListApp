using CarListApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Services
{
    public class CarService
    {
        private SQLiteConnection conn;
        private string dbPath;
        public string StatusMessage = "";

        public CarService(string dbPath)
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
    }
}
