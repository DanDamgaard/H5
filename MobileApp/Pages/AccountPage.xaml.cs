using MobileApp.Services;

namespace MobileApp.Pages;

public partial class AccountPage : ContentPage
{
    private AuthService _authService = new AuthService();
	public AccountPage()
	{
		InitializeComponent();
		UserLabel.Text = Global.User.Name;
	}

    private async void logoutBtn_Clicked(object sender, EventArgs e)
    {
        _authService.logout();
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}