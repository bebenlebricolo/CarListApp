using CarListApp.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using CarListApp.ViewModels;
using System.Net.Http.Headers;
using System.Diagnostics;
using static Google.Apis.Requests.BatchRequest;
using System.IdentityModel.Tokens.Jwt;

namespace CarListApp.Services
{
    public class CarApiService
    {
        HttpClient _httpClient;
        public string StatusMessage;

        public CarApiService()
        {
            var baseAddress = GetBaseAdress();
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler) { BaseAddress = new Uri(baseAddress) };
#else
            _httpClient = new() { BaseAddress = new Uri(baseAddress) };
#endif

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
            
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            
            // Check if token still valid, if expired, then renew credentials
            if(jwt.ValidTo < DateTime.UtcNow)
            {
                var loginModel = new LoginModel(App.UserInfo.Username, App.UserInfo.Password, 1);
                
                var authResponse = await Login(loginModel);
                token = authResponse.AccessToken;
                await SecureStorage.SetAsync("AccessToken", token);
            }


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
