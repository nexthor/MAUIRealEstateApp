using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IApiService _apiService;

        public MainPage(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var login = await _apiService.LoginAsync(new Login { Email = "elon@email.com", Password = "Elon1234" });

            // when the user logs in, the token should be stored in preferences
            Preferences.Set(RealEstateConstants.AccessToken, login.Token);

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
