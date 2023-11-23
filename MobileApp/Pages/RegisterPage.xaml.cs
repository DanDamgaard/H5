using MobileApp.Classes;
using MobileApp.Services;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace MobileApp.Pages;

public partial class RegisterPage : ContentPage
{
	private Api api = new Api();
    public RegisterPage()
	{
		InitializeComponent();
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

		if(await api.doesEmailExit(emailBox.Text))
		{
			await DisplayAlert("Fejl", "E-mail er blevet brugt før skriv en anden E-mail", "OK");
			return;
		}

		string hashPass = Hash(passBox.Text);
		
		User user = new User(0,nameBox.Text, hashPass, emailBox.Text.ToLower(), phoneBox.Text, addressBox.Text, 0);

		if(await api.CreateUser(user))
		{
			await DisplayAlert("Succes", "Din bruger er blevet oprettet", "OK");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
		else
		{
			await DisplayAlert("Fejl", "Kunne ikke lave bruger prøv igen", "OK");
			return;
		}
		

	}
}