using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly AuthService _authService = new AuthService();
    private Api api = new Api();
    
    public MainPage()
    {
        InitializeComponent();
        lateBooksWarning();
    }

    private async void lateBooksWarning()
    {
        List<RentedBook> lateBooks = await api.getOverdueBooks();
        List<RentedBook> userLateBooks = new List<RentedBook>();

        if (lateBooks.Count > 0 )
        {
            foreach(RentedBook rb in lateBooks)
            {
                if(rb.UserId == Global.User.Id)
                {
                    userLateBooks.Add(rb);
                }
            }
        }

        foreach(RentedBook rb in userLateBooks)
        {
            Book book = await api.getBook(rb.BookId);
            await DisplayAlert("Advarsel", $"{book.Title} skulle være afleveret den {rb.EndDate}", "OK");
        }
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

    private async void RentedBooksBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new UserRentedBooksPage(Global.User));
    }

    private async void RentHistory_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new UserRentHistoryPage(Global.User));
    }
}