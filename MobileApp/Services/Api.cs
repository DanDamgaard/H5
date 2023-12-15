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

        //private string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
        //? "http://10.0.2.2:5131"
        //: "http://localhost:5131";

        private string baseUrl = "http://dantdamgaard.dk";

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


        public async Task<List<Book>> GetBooks()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/Book/GetBooks");
            var result = await response.Content.ReadAsStringAsync();
            List<Book> data = JsonConvert.DeserializeObject<List<Book>>(result);
            return data;
        }

        public async Task<bool> createBook(BookUpload book)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/Book/AddBook", book);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> editBook(int id,BookUpload book)
        {
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/api/Book/UpdateBook/{id}", book);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> deleteBook(int id)
        {
            var response = await httpClient.DeleteAsync($"{baseUrl}/api/Book/DeleteBook/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Book> getBook(int id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/Book/GetBook/{id}");
            var result = await response.Content.ReadAsStringAsync();
            Book data = JsonConvert.DeserializeObject<Book>(result);
            return data;
        }

        public async Task<List<RentedBook>> getRentedBooks()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/UserBook/GetRentedBooks");
            var result = await response.Content.ReadAsStringAsync();
            List<RentedBook> data = JsonConvert.DeserializeObject<List<RentedBook>>(result);
            return data;
        }

        public async Task<List<RentedBook>> getUsersAndRentedBooks()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/UserBook/GetUsersAndBooks");
            var result = await response.Content.ReadAsStringAsync();
            List<RentedBook> data = JsonConvert.DeserializeObject<List<RentedBook>>(result);
            return data;
        }

        public async Task<List<RentedBook>> getOverdueBooks()
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/UserBook/GetOverdueBooks");
            var result = await response.Content.ReadAsStringAsync();
            List<RentedBook> data = JsonConvert.DeserializeObject<List<RentedBook>>(result);
            return data;
        }

        public async Task<bool> returnBook(RentedBook book)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/UserBook/ReturnBook", book);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> rentBook(RentedBook book)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/UserBook/RentBook", book);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> reRentBook(RentedBook book)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/UserBook/ReRentBook", book);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
