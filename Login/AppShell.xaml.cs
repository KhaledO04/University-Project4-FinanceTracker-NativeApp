using MinApp.Views;

namespace MinApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("JobOverviewPage", typeof(JobOverviewPage));
            Routing.RegisterRoute("AddJobPage", typeof(AddJobPage));
            Routing.RegisterRoute("LogHoursPage", typeof(LogHoursPage));
            Routing.RegisterRoute("AddHoursPage", typeof(AddHoursPage));
            Routing.RegisterRoute("PaycheckPage", typeof(PaycheckPage));
            Routing.RegisterRoute("HolidayPayPage", typeof(HolidayPayPage));
            Routing.RegisterRoute("StudentGrantPage", typeof(StudentGrantPage));
        }
    }
}