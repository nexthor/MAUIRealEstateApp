using RealEstate.Pages;

namespace RealEstate
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetService<RegisterPage>();
        }
    }
}
