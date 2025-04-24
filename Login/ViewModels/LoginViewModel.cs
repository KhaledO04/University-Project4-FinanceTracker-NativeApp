using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using MinApp.Services;
using MinApp.Models;
using MinApp.Services;

namespace MinApp.ViewModels;

// ViewModel til Login-siden: indeholder brugerens inputdata, fejlbesked og kommando til at logge ind
public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthService _authService;
    private string _email;
    private string _adgangskode;
    private string _fejlbesked;

    public string Email
    {
        get => _email;
        set { if (value != _email) { _email = value; OnPropertyChanged(); } }
    }

    public string Adgangskode
    {
        get => _adgangskode;
        set { if (value != _adgangskode) { _adgangskode = value; OnPropertyChanged(); } }
    }

    public string Fejlbesked
    {
        get => _fejlbesked;
        set { if (value != _fejlbesked) { _fejlbesked = value; OnPropertyChanged(); } }
    }

    public ICommand LogIndKommando { get; }

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
        LogIndKommando = new Command(async () => await LogIndAsync());
        _email = string.Empty;
        _adgangskode = string.Empty;
        _fejlbesked = string.Empty;
    }

    private async Task LogIndAsync()
    {
        Fejlbesked = string.Empty;
        try
        {
            // Forsøg at logge ind via AuthService
            User bruger = await _authService.LoginAsync(Email, Adgangskode);
            if (bruger != null)
            {
                // Navigation til HomePage efter vellykket login (rydder navigationsstakken)
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                Fejlbesked = "Login mislykkedes. Tjek email og adgangskode.";
            }
        }
        catch (Exception ex)
        {
            // Håndter uventede fejl (fx netværk)
            Fejlbesked = $"Der opstod en fejl: {ex.Message}";
        }
    }

    // INotifyPropertyChanged implementation for data-binding
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
