using MobileApp.Services;

namespace MobileApp.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
		UserLabel.Text = Global.User.Name;
	}
}