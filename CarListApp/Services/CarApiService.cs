using CarListApp.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using CarListApp.ViewModels;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace CarListApp.Services
{
    public class CarApiService
    {
        HttpClient _httpClient;
        public string StatusMessage;

        public CarApiService()
        {
            var baseAddress = GetBaseAdress();
            _httpClient = new() { BaseAddress = new Uri(baseAddress) };
        }

        private string GetBaseAdress()
        {
            #if DEBUG
                return DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:8099" : "https://localhost:8099";
            #elif RELEASE
                // published address here
                return "https://carlistappapi20221121135717.azurewebsites.net";
            #endif
        }

        public async Task <List<Car>> GetCars()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync("cars");
                return JsonConvert.DeserializeObject<List<Car>>(response);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync("cars/" + id);
                return JsonConvert.DeserializeObject<Car>(response);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task AddCar(Car car)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.PostAsJsonAsync("cars/", car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to add data.";
            }
        }

        public async Task DeleteCar(int id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.DeleteAsync("cars/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Delete Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.PutAsJsonAsync("cars/" + id, car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to update data.";
            }
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("login", loginModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Login successful";
                return JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
                StatusMessage = "Failed to login.";
            }
            return default;
        }

        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
