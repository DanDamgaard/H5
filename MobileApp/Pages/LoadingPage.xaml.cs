using MobileApp.Services;

namespace MobileApp.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.isAuthenticatedAsync())
        {
            // hvis man er logget in redirect til mainPage
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

        }
        else
        {
            //hvis man ikke er logget in redirect til loginPage
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}