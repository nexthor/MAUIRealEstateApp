using RealEstate.Pages;

namespace RealEstate.Extensions
{
    public static class PagesExtension
    {
        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<RegisterPage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<HomePage>();

            return services;
        }
    }
}
