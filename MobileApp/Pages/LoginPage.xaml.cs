using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    private async void login()
    {
        User user = new User(1, "User", "Password", "email", 123456798, "address", false);

        if (emailBox.Text == user.Email && passBox.Text == user.Password)
        {
            _authService.login(user.Id);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        emailBox.Text = "";
        passBox.Text = "";
    }
    
    private void LoginBtn_Clicked(object sender, EventArgs e)
    {
        login();
    }

    private void passBox_Completed(object sender, EventArgs e)
    {
        login();
    }

    private void emailBox_Completed(object sender, EventArgs e)
    {
        passBox.Focus();
    }
}