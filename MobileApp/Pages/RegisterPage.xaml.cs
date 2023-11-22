using MobileApp.Classes;
using MobileApp.Services;
using System.Net.Http.Json;

namespace MobileApp.Pages;

public partial class RegisterPage : ContentPage
{
	private Api api;
    public RegisterPage()
	{
		InitializeComponent();
    }

    private async void registerBtn_Clicked(object sender, EventArgs e)
    {
		HttpClient client = new HttpClient();

		if (nameValidator.IsNotValid)
		{
			await DisplayAlert("Fejl", "Du skal skrive din fulde navn", "Ok");
			return;
		}

		if (addressValidator.IsNotValid)
		{
			await DisplayAlert("Fejl", "Du skal skrive en adresse", "OK");
			return;
		}

		if (String.IsNullOrEmpty(phoneBox.Text) || int.Parse(phoneBox.Text) < 11111111)
		{
			await DisplayAlert("Fejl", "Du skal skrive en gyldig Tlf. nr", "OK");
			return;
		}

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

		if(passBox.Text != repeatBox.Text)
		{
			await DisplayAlert("Fejl", "Dine adgangskoder skal være ens", "OK");
			return;
		}

		
		User user = new User(0,nameBox.Text,passBox.Text,emailBox.Text,phoneBox.Text,addressBox.Text, 0);

		if(await api.CreateUser(user))
		{
			await DisplayAlert("Succes", "Din bruger er blevet oprettet", "OK");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
		

	}
}