using MinApp.Services;
using Microsoft.Maui.Controls;

namespace MinApp.Views;

public partial class HomePage : ContentPage
{
    private readonly AuthService _authService;

    public HomePage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
        // Vis en velkomsthilsen med brugerens email, hvis brugerdata er tilgængelig
        var bruger = _authService.GetCurrentUser();
        if (bruger != null)
            WelcomeLabel.Text = $"Velkommen, {bruger.Email}";
    }
}
