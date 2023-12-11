using MobileApp.Services;

namespace MobileApp.Pages;

public partial class AdminMainPage : ContentPage
{
    private AuthService _authService = new AuthService();
	public AdminMainPage()
	{
		InitializeComponent();
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

    private async void UsersBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AllUsersPage());
    }

    private async void BookListBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BookListPage());
    }

    private async void CreateBookBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AdminCreateBook());
    }
}