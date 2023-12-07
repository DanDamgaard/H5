using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class AllUsersPage : ContentPage
{
	List<User> users = new List<User>();
	Api api = new Api();
	public AllUsersPage()
	{
		InitializeComponent();
		getUsers();
	}

	private async void getUsers()
	{
		users = await api.getAllUser();

		foreach (User user in users)
		{
			if(user.Roles == 0)
			{
                VerticalStackLayout stack = new VerticalStackLayout();
				stack.Spacing = 10;
                stack.HorizontalOptions = LayoutOptions.Center;

                Label label = new Label();
				label.FontAttributes = FontAttributes.Bold;
				label.FontSize = 14;
                label.Text = user.Name;
				label.HorizontalOptions = LayoutOptions.Center;


				Button button = new Button();
				button.FontAttributes = FontAttributes.Bold;
				button.Text = "Se detaljer";
				button.Clicked += (s, arg) =>
				{
					Navigation.PushModalAsync(new AdminUserDetails(user));
				};
				
                


                stack.Children.Add(label);
				stack.Children.Add(button);

                UserStack.Children.Add(stack);
            }
		}
	}
}