using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<string> EmploymentTypes { get; } = new ObservableCollection<string>
        {
            "Full-time",
            "Part-time",
            "Hourly",
            "Contract",
            "Internship",
            "Student job"
        };

        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set => SetProperty(ref _jobName, value);
        }

        private string _employmentType;
        public string EmploymentType
        {
            get => _employmentType;
            set => SetProperty(ref _employmentType, value);
        }

        private bool _isBoard;
        public bool IsBoard
        {
            get => _isBoard;
            set => SetProperty(ref _isBoard, value);
        }

        private bool _isCard;
        public bool IsCard
        {
            get => _isCard;
            set => SetProperty(ref _isCard, value);
        }

        private string _taxNumber;
        public string TaxNumber
        {
            get => _taxNumber;
            set => SetProperty(ref _taxNumber, value);
        }

        private string _hourlyRate;
        public string HourlyRate
        {
            get => _hourlyRate;
            set => SetProperty(ref _hourlyRate, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddJobViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Add Job";

            // Set default values
            EmploymentType = EmploymentTypes.FirstOrDefault();

            SaveCommand = new Command(async () => await SaveJobAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async Task SaveJobAsync()
        {
            if (string.IsNullOrWhiteSpace(JobName))
            {
                ErrorMessage = "Please enter a job name";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var job = new Job
                {
                    Name = JobName,
                    EmploymentType = EmploymentType,
                    IsBoard = IsBoard,
                    IsCard = IsCard,
                    TaxNumber = TaxNumber,
                    HourlyRate = HourlyRate
                };

                var result = await _apiService.AddJobAsync(job);

                if (result != null)
                {
                    // Navigate back to job overview page
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    ErrorMessage = "Failed to add job. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to add job: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            // Navigate back to job overview page
            await Shell.Current.GoToAsync("..");
        }
    }
}