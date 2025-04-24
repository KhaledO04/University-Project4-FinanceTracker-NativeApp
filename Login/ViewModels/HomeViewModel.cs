using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<string> MenuItems { get; } = new ObservableCollection<string>
        {
            "Job overview",
            "Log hours",
            "Paycheck",
            "Holiday pay",
            "Student grant"
        };

        public ICommand MenuItemSelectedCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "FinanceTracker";

            MenuItemSelectedCommand = new Command<string>(async (item) => await MenuItemSelectedAsync(item));
            LogoutCommand = new Command(async () => await LogoutAsync());
        }

        private async Task MenuItemSelectedAsync(string item)
        {
            switch (item)
            {
                case "Job overview":
                    await Shell.Current.GoToAsync("JobOverviewPage");
                    break;
                case "Log hours":
                    await Shell.Current.GoToAsync("LogHoursPage");
                    break;
                case "Paycheck":
                    await Shell.Current.GoToAsync("PaycheckPage");
                    break;
                case "Holiday pay":
                    await Shell.Current.GoToAsync("HolidayPayPage");
                    break;
                case "Student grant":
                    await Shell.Current.GoToAsync("StudentGrantPage");
                    break;
            }
        }

        private async Task LogoutAsync()
        {
            // Clear token and navigate to login page
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}