using MobileApp.Classes;

namespace MobileApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void registerBtn_Clicked(object sender, EventArgs e)
    {
		if (nameValidator.IsNotValid)
		{
			DisplayAlert("Fejl", "Du skal skrive din fulde navn", "Ok");
			return;
		}

		if (addressValidator.IsNotValid)
		{
			DisplayAlert("Fejl", "Du skal skrive en adresse", "OK");
			return;
		}

		if (String.IsNullOrEmpty(phoneBox.Text) || int.Parse(phoneBox.Text) < 11111111)
		{
			DisplayAlert("Fejl", "Du skal skrive en gyldig Tlf. nr", "OK");
			return;
		}

		if (emailValidator.IsNotValid)
		{
			foreach (var error in emailValidator.Errors)
			{
				DisplayAlert("Fejl", error.ToString(), "OK");
			}
			return;
		}

		if (passValidator.IsNotValid)
		{
			foreach (var error in passValidator.Errors)
			{
				DisplayAlert("Fejl", error.ToString(), "OK");
			}

			return;
		}

		if(passBox.Text != repeatBox.Text)
		{
			DisplayAlert("Fejl", "Dine adgangskoder skal være ens", "OK");
			return;
		}

		User user = new User(0,nameBox.Text,passBox.Text,emailBox.Text,int.Parse(phoneBox.Text),addressBox.Text,false);


	}
}