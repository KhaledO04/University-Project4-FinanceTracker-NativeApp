using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MinApp.ViewModels
{
    public class HolidayPayViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Job> Jobs { get; } = new ObservableCollection<Job>();
        public ObservableCollection<HolidayPay> HolidayPays { get; } = new ObservableCollection<HolidayPay>();

        private Job _selectedJob;
        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                if (SetProperty(ref _selectedJob, value))
                {
                    LoadHolidayPaysAsync().ConfigureAwait(false);
                }
            }
        }

        public HolidayPayViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Holiday Pay";
        }

        public async Task OnAppearing()
        {
            await LoadJobsAsync();
        }

        private async Task LoadJobsAsync()
        {
            if (IsBusy)
                return;

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
                // Handle error
                Debug.WriteLine($"Error loading jobs: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadHolidayPaysAsync()
        {
            if (IsBusy || SelectedJob == null)
                return;

            IsBusy = true;

            try
            {
                HolidayPays.Clear();
                var holidayPays = await _apiService.GetHolidayPayAsync(SelectedJob.Id);

                foreach (var holidayPay in holidayPays)
                {
                    HolidayPays.Add(holidayPay);
                }
            }
            catch (Exception ex)
            {
                // Handle error
                Debug.WriteLine($"Error loading holiday pays: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}