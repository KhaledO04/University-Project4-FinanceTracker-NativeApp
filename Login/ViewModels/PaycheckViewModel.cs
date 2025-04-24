using MinApp.Models;
using MinApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class PaycheckViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Job> Jobs { get; } = new ObservableCollection<Job>();
        public ObservableCollection<Paycheck> Paychecks { get; } = new ObservableCollection<Paycheck>();

        private Job _selectedJob;
        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                if (SetProperty(ref _selectedJob, value))
                {
                    LoadPaychecksAsync().ConfigureAwait(false);
                }
            }
        }

        public ICommand PaycheckSelectedCommand { get; }

        public PaycheckViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Paychecks";

            PaycheckSelectedCommand = new Command<Paycheck>(async (paycheck) => await OnPaycheckSelectedAsync(paycheck));
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

        private async Task LoadPaychecksAsync()
        {
            if (IsBusy || SelectedJob == null)
                return;

            IsBusy = true;

            try
            {
                Paychecks.Clear();
                var paychecks = await _apiService.GetPaychecksAsync(SelectedJob.Id);

                foreach (var paycheck in paychecks)
                {
                    Paychecks.Add(paycheck);
                }
            }
            catch (Exception ex)
            {
                // Handle error
                Debug.WriteLine($"Error loading paychecks: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnPaycheckSelectedAsync(Paycheck paycheck)
        {
            if (paycheck == null)
                return;

            // Navigate to paycheck details page with the selected paycheck
            var navigationParameter = new Dictionary<string, object>
            {
                { "Paycheck", paycheck }
            };

            // For now, we'll just display an alert with the paycheck details
            await Shell.Current.DisplayAlert(
                "Paycheck Details",
                $"Job: {paycheck.JobName}\n" +
                $"Period: {paycheck.PayPeriodStart:dd/MM/yyyy} - {paycheck.PayPeriodEnd:dd/MM/yyyy}\n" +
                $"Hours: {paycheck.HoursWorked}\n" +
                $"Gross Pay: {paycheck.GrossPay:C2}\n" +
                $"Net Pay: {paycheck.NetPay:C2}\n" +
                $"Tax Withheld: {paycheck.TaxWithheld:C2}\n" +
                $"AM Bidrag: {paycheck.AMBidrag:C2}",
                "OK");
        }
    }
}