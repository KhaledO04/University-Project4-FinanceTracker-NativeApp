using Microsoft.Maui.Controls;

namespace MinApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Her kan man registrere yderligere routes programmatisk hvis nødvendigt:
        // Routing.RegisterRoute("SomePage", typeof(Views.SomePage));
    }
}
