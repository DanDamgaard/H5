using MobileApp.Services;

namespace MobileApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly AuthService _authService;

    public MainPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
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

    private async void logoutBtn_Clicked(object sender, EventArgs e)
    {
        _authService.logout();
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}