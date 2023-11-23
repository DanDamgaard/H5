using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class ProfilPage : ContentPage
{
	public ProfilPage()
	{
		InitializeComponent();
		
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        nameLabel.Text = Global.User.Name;
        addressLabel.Text = Global.User.Address;
        phoneLabel.Text = Global.User.Phone;

    }

    private async void toUpdateProfilBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new EditProfilPage());
    }
}