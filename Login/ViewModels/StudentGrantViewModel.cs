using System.Diagnostics;
using MinApp.Models;
using MinApp.Services;

namespace MinApp.ViewModels
{
    public class StudentGrantViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private StudentGrant _studentGrant;
        public StudentGrant StudentGrant
        {
            get => _studentGrant;
            set => SetProperty(ref _studentGrant, value);
        }

        public StudentGrantViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Student Grant";
            StudentGrant = new StudentGrant(); // Initialize with empty object
        }

        public async Task OnAppearing()
        {
            await LoadStudentGrantAsync();
        }

        private async Task LoadStudentGrantAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                StudentGrant = await _apiService.GetStudentGrantAsync();
            }
            catch (Exception ex)
            {
                // Handle error
                Debug.WriteLine($"Error loading student grant: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}