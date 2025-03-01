using RealEstate.Pages;

namespace RealEstate
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();
            var loginPage = services.GetService<LoginPage>();
            var tabbedPage = services.GetService<CustomTabPage>();
            var token = Preferences.Get(RealEstateConstants.AccessToken, string.Empty);
            Page? page = string.IsNullOrEmpty(token) ? loginPage : tabbedPage;

            MainPage = new NavigationPage(page);
        }
    }
}
