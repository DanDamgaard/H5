using static System.Net.WebRequestMethods;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
    
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCallApiBtnClicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                 ? "http://10.0.2.2:5131"
                 : "http://localhost:5131";

           var response = await httpClient.GetAsync($"{baseUrl}/WeatherForecast");

            var data = await response.Content.ReadAsStringAsync();
        }
  
    }

}
