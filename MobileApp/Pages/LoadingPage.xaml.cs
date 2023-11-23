using MobileApp.Services;

namespace MobileApp.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;
    private readonly Api _api;

    public LoadingPage(AuthService authService, Api api)
	{
		InitializeComponent();
        _authService = authService;
        _api = api;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.isAuthenticatedAsync())
        {
            string user = Preferences.Default.Get("EmailKey", "Unknown");
            string pass = Preferences.Default.Get("PassKey", "Unknown");

           if (await _api.Login(user, pass))
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                _authService.logout();
            }
            // hvis man er logget in redirect til mainPage
            

        }
        else
        {
            //hvis man ikke er logget in redirect til loginPage
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}