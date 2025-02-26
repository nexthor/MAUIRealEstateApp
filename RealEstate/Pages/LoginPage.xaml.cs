using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IApiService _apiService;
    public LoginPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        var login = new Login
        {
            Email = entEmail.Text,
            Password = entPassword.Text,
        };

        try
        {
            await _apiService.LoginAsync(login);

            entEmail.Text = null;
            entPassword.Text = null;

            await DisplayAlert("Success", "Welcome back!", "Ok");

            var homePage = ProvideService.GetService<CustomTabPage>();
            await Navigation.PushAsync(homePage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var registerPage = ProvideService.GetService<RegisterPage>();
        await Navigation.PushAsync(registerPage);
    }
}