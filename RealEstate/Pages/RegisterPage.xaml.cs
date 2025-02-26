using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Pages;

public partial class RegisterPage : ContentPage
{
	private readonly IApiService _apiService;
    public RegisterPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        var register = new Register
        {
            Email = entEmail.Text,
            Password = entPassword.Text,
            Name = entFullName.Text,
            Phone = entPhone.Text
        };

        try
        {
            await _apiService.RegisterUser(register);

            entEmail.Text = null;
            entPassword.Text = null;
            entFullName.Text = null;
            entPhone.Text = null;

            await DisplayAlert("Success", "User registered successfully", "Ok");

            var loginPage = ProvideService.GetService<LoginPage>();
            await Shell.Current.Navigation.PushAsync(loginPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var loginPage = ProvideService.GetService<LoginPage>();
        await Shell.Current.Navigation.PushAsync(loginPage);
    }
}