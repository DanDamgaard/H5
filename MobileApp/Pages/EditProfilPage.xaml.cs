using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class EditProfilPage : ContentPage
{
    User currentUser = Global.User;
    Api api = new Api();
	public EditProfilPage()
	{
		InitializeComponent();
        nameBox.Text = Global.User.Name;
        addressBox.Text = Global.User.Address;
        phoneBox.Text = Global.User.Phone;

    }

    private async void UpdateProfilBtn_Clicked(object sender, EventArgs e)
    {
        User newUser = new User(currentUser.Id,currentUser.Name,currentUser.Password,currentUser.Email,currentUser.Phone,currentUser.Address,currentUser.Roles);

        //User newUser = Global.User;

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

        

        newUser.Name = nameBox.Text;
        newUser.Address = addressBox.Text;
        newUser.Phone = phoneBox.Text;

        if (Global.User == newUser)
        {
            await DisplayAlert("Fejl", "Du har ikke lavet nogen ændringer", "OK");
        }

        if (await api.updateUser(newUser))
        {
            await DisplayAlert("Succes", "Profil er blevet opdateret", "OK");
            Global.User = newUser;
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Fejl", "Nået gik galt prøv igen", "OK");
        }

    }
}