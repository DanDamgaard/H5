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
        await Navigation.PushModalAsync(new LoginPage());

    }

    private async void profilBtn_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushModalAsync(new ProfilPage());
       await Navigation.PushModalAsync(new ProfilPage());
    }

    private async void editLoginBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditLoginPage());
    }
}