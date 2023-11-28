using MobileApp.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class Api
    {
        private HttpClient httpClient = new HttpClient();
        private string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
             ? "http://10.0.2.2:5131"
             : "http://localhost:5131";

        public async Task<bool> Login(string username, string password)
        {
            string email = username;
            email.Replace("@", "%40");

            var response = await httpClient.GetAsync($"{baseUrl}/api/User/Login/{email}/{password}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                User data = JsonConvert.DeserializeObject<User>(result);
                Global.User = data;
                return true;
            }else
            {
                return false;
            }
        }

        public async Task<bool> CreateUser(User user)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/User/AddUser",user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> getUser(int id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/User/GetUser/{id}");
            var result = await response.Content.ReadAsStringAsync();
            User data = JsonConvert.DeserializeObject<User>(result);
            return data;
           
        }

        public async Task<List<User>> getAllUser()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/User/GetUsers");
            var result = await response.Content.ReadAsStringAsync();
            List<User> data = JsonConvert.DeserializeObject<List<User>>(result);
            return data;

        }



        public async Task<bool> doesEmailExit(string email)
        {
            email.Replace("@", "%40");

            var response = await httpClient.GetAsync($"{baseUrl}/api/User/CheckEmail/{email}");

            var result = await response.Content.ReadAsStringAsync();

            bool answer = JsonConvert.DeserializeObject<bool>(result);

            return answer;
        }

        public async Task<bool> updateUser(User user)
        {
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/api/User/UpdateUser/{user.Id}",user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task deleteUser(int id)
        {
            var response = await httpClient.DeleteAsync($"{baseUrl}/api/User/DeleteUser/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.RequestMessage.ToString());
            }
        }






    }
}
