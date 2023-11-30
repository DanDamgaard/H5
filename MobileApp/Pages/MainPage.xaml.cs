using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly AuthService _authService = new AuthService();

    
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

    private void logoutBtn_Clicked(object sender, EventArgs e)
    {
        _authService.logout();
        Application.Current.MainPage = new LoginPage();
    }

    private async void userBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AccountPage());
    }

    private async void BookListBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BookListPage());
    }
}