using MobileApp.Classes;
using MobileApp.Services;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace MobileApp.Pages;

public partial class EditLoginPage : ContentPage
{
    User currentUser = Global.User;
    Api api = new Api();
	public EditLoginPage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        emailBox.Text = Global.User.Email;

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

    private async void updateUserBtn_Clicked(object sender, EventArgs e)
    {
        User newUser = new User(currentUser.Id, currentUser.Name, currentUser.Password, currentUser.Email, currentUser.Phone, currentUser.Address, currentUser.Roles);

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

        if (passBox.Text != repeatBox.Text)
        {
            await DisplayAlert("Fejl", "Dine adgangskoder skal være ens", "OK");
            return;
        }

        newUser.Email = emailBox.Text;
        string newPass = Hash(passBox.Text);

        newUser.Password = newPass;

        if(currentUser == newUser)
        {
            await DisplayAlert("Fejl", "Du har ikke lavet nogen ændinger", "OK");
            return;
        }

        if(await api.updateUser(newUser))
        {
            await DisplayAlert("Succes", "Login blevet opdateret", "OK");
            Global.User = newUser;
            await Navigation.PopModalAsync();
        }


    }
}