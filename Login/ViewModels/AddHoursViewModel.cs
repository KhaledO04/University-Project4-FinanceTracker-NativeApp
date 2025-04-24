using MinApp.Models;
using MinApp.Services;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    [QueryProperty(nameof(JobId), "JobId")]
    public class AddHoursViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private int _jobId;
        public int JobId
        {
            get => _jobId;
            set => SetProperty(ref _jobId, value);
        }

        private DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private TimeSpan _startTime = new TimeSpan(9, 0, 0);
        public TimeSpan StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        private TimeSpan _endTime = new TimeSpan(17, 0, 0);
        public TimeSpan EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddHoursViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Add Hours";

            SaveCommand = new Command(async () => await SaveHoursAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async Task SaveHoursAsync()
        {
            if (EndTime <= StartTime)
            {
                ErrorMessage = "End time must be after start time";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var workHours = new WorkHours
                {
                    JobId = JobId,
                    Date = Date,
                    StartTime = StartTime,
                    EndTime = EndTime,
                    IsPaid = false
                };

                var result = await _apiService.AddWorkHoursAsync(workHours);

                if (result != null)
                {
                    // Navigate back to log hours page
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    ErrorMessage = "Failed to add hours. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to add hours: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            // Navigate back to log hours page
            await Shell.Current.GoToAsync("..");
        }
    }
}