using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class LogHoursViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Job> Jobs { get; } = new ObservableCollection<Job>();
        public ObservableCollection<WorkHours> WorkHours { get; } = new ObservableCollection<WorkHours>();

        private Job _selectedJob;
        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                if (SetProperty(ref _selectedJob, value) && value != null)
                {
                    LoadWorkHoursCommand.Execute(value.Id);
                }
            }
        }

        public ICommand LoadJobsCommand { get; }
        public ICommand LoadWorkHoursCommand { get; }
        public ICommand AddHoursCommand { get; }

        public LogHoursViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Log Hours";

            LoadJobsCommand = new Command(async () => await LoadJobsAsync());
            LoadWorkHoursCommand = new Command<int>(async (jobId) => await LoadWorkHoursAsync(jobId));
            AddHoursCommand = new Command(async () => await GoToAddHoursAsync());
        }

        private async Task LoadJobsAsync()
        {
            IsBusy = true;

            try
            {
                Jobs.Clear();
                var jobs = await _apiService.GetJobsAsync();

                foreach (var job in jobs)
                {
                    Jobs.Add(job);
                }

                if (Jobs.Count > 0 && SelectedJob == null)
                {
                    SelectedJob = Jobs[0];
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load jobs: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadWorkHoursAsync(int jobId)
        {
            if (jobId <= 0)
                return;

            IsBusy = true;

            try
            {
                WorkHours.Clear();
                var hours = await _apiService.GetWorkHoursAsync(jobId);

                foreach (var hour in hours)
                {
                    WorkHours.Add(hour);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load work hours: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToAddHoursAsync()
        {
            if (SelectedJob == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select a job first", "OK");
                return;
            }

            var parameters = new Dictionary<string, object>
            {
                { "JobId", SelectedJob.Id }
            };

            await Shell.Current.GoToAsync("AddHoursPage", parameters);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadJobsCommand.Execute(null);
        }
    }
}