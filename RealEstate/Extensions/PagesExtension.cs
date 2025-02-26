using RealEstate.Pages;

namespace RealEstate.Extensions
{
    public static class PagesExtension
    {
        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<RegisterPage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<HomePage>();
            services.AddTransient<BookmarksPage>();
            services.AddTransient<CustomTabPage>();
            services.AddTransient<SettingsPage>();

            return services;
        }
    }
}
