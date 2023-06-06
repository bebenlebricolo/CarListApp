using CarListApp.Models;
using CarListApp.Services;
using CarListApp.ViewModels;
using CarListApp.Views;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;

namespace CarListApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string dbPath = Path.GetFullPath("C:\\CarListApi\\carlist.db");

        // ViewModels
        builder.Services.AddTransient<CarListViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();
        builder.Services.AddTransient<Car>();

        // Services
        builder.Services.AddTransient<CarApiService>();
        builder.Services.AddTransient(s => ActivatorUtilities.CreateInstance<CarDatabaseService>(s, dbPath));

        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
