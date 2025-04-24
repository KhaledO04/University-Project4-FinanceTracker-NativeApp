using Foundation;

namespace Login
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MinApp.MauiProgram.CreateMauiApp();
    }
}
