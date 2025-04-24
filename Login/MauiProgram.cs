using MinApp.Converters;
using MinApp.Services;
using MinApp.ViewModels;
using MinApp.Views;
using Microsoft.Extensions.Logging;

namespace MinApp
{
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

            // Register services
            // Use MockApiService for testing without a backend
            //builder.Services.AddSingleton<IApiService, MockApiService>();
            // Use real ApiService when backend is ready
            builder.Services.AddSingleton<IApiService, ApiService>();

            // Register view models
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<JobOverviewViewModel>();
            builder.Services.AddTransient<AddJobViewModel>();
            builder.Services.AddTransient<LogHoursViewModel>();
            builder.Services.AddTransient<AddHoursViewModel>();
            builder.Services.AddTransient<PaycheckViewModel>();
            builder.Services.AddTransient<HolidayPayViewModel>();
            builder.Services.AddTransient<StudentGrantViewModel>();

            // Register pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<JobOverviewPage>();
            builder.Services.AddTransient<AddJobPage>();
            builder.Services.AddTransient<LogHoursPage>();
            builder.Services.AddTransient<AddHoursPage>();
            builder.Services.AddTransient<PaycheckPage>();
            builder.Services.AddTransient<HolidayPayPage>();
            builder.Services.AddTransient<StudentGrantPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}