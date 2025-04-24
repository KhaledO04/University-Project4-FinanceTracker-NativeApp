using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MinApp.Services;
using MinApp.ViewModels;
using MinApp.Views;

namespace MinApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>(); // Registrér App-klassen som roden af MAUI-appen

        // Dependency Injection konfiguration
        // Registrér HttpClient med base-URL til web API (indsæt korrekt URL til API’et):
        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5140/")
        });
        // Registrér services og ViewModels:
        builder.Services.AddSingleton<AuthService>();       // Godkendelsesservice (JWT håndtering)
        builder.Services.AddTransient<LoginViewModel>();    // ViewModel til login-siden
        // Registrér views (sider) for navigation via Shell:
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}
