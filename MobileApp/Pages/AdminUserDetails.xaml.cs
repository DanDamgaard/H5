using MobileApp.Classes;

namespace MobileApp.Pages;

public partial class AdminUserDetails : ContentPage
{
    private readonly User _user;

    public AdminUserDetails(User user)
	{
		InitializeComponent();
        _user = user;
        nameLabel.Text = "Navn: " + _user.Name;
        emailLabel.Text = "E-mail: " + _user.Email;
        phoneLabel.Text = "Tfl: " + _user.Phone;
        addressLabel.Text = "Adresse: " + _user.Address;
    }

    private void rentHistoryBtn_Clicked(object sender, EventArgs e)
    {

    }
}