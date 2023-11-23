using MobileApp.Classes;
using MobileApp.Services;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace MobileApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;
    private readonly Api _api;

    public LoginPage(AuthService authService, Api api)
	{
		InitializeComponent();
        _authService = authService;
        _api = api;
    }

    private string Hash(string str)
    {
        StringBuilder Sb = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(str));

            foreach (byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    private async void login()
    {

        if (emailValidator.IsNotValid)
        {
            foreach (var error in emailValidator.Errors)
            {
                await DisplayAlert("Fejl", error.ToString(), "OK");
            }
            return;
        }

        if (passValidator.IsNotValid)
        {
            foreach (var error in passValidator.Errors)
            {
                await DisplayAlert("Fejl", error.ToString(), "OK");
            }

            return;
        }

        string hashPass = Hash(passBox.Text);
           
        if(await _api.Login(emailBox.Text, hashPass)){

            Preferences.Default.Set("EmailKey", emailBox.Text);
            Preferences.Default.Set("PassKey", hashPass);

            emailBox.Text = "";
            passBox.Text = "";

            _authService.login();


            string eKey = emailBox.Text;

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            await DisplayAlert("Login Fejlet", "Kan ikke finde bruger tjek om du har skrevet din login rigtigt", "OK");
        }
        

        
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

    private async void registerBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}