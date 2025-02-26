using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RealEstate.Services;
using System.Reflection;

namespace RealEstate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder(true);
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("RealEstate.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream!)
                        .Build();

            builder.Configuration.AddConfiguration(config);
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IApiService, ApiService>();
            builder.Services.AddScoped<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            ProvideService.Instance = app.Services;

            return app;
        }
    }
}
