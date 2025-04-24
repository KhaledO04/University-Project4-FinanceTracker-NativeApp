using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class JobOverviewViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Job> Jobs { get; } = new ObservableCollection<Job>();

        public ICommand LoadJobsCommand { get; }
        public ICommand AddJobCommand { get; }
        public ICommand JobSelectedCommand { get; }

        public JobOverviewViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Job Overview";

            LoadJobsCommand = new Command(async () => await LoadJobsAsync());
            AddJobCommand = new Command(async () => await GoToAddJobAsync());
            JobSelectedCommand = new Command<Job>(async (job) => await JobSelectedAsync(job));
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

        private async Task GoToAddJobAsync()
        {
            await Shell.Current.GoToAsync("AddJobPage");
        }

        private async Task JobSelectedAsync(Job job)
        {
            if (job == null)
                return;

            // Navigate to job details page
            var parameters = new Dictionary<string, object>
            {
                { "JobId", job.Id }
            };

            await Shell.Current.GoToAsync("JobDetailsPage", parameters);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadJobsCommand.Execute(null);
        }
    }
}