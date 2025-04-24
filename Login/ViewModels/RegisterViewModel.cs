using MinApp.Models;
using MinApp.Services;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        public RegisterViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Register";

            RegisterCommand = new Command(async () => await RegisterAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please fill in all required fields";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var user = new User
                {
                    FullName = FullName,
                    Email = Email,
                    Phone = Phone,
                    Password = Password
                };

                var success = await _apiService.RegisterAsync(user);

                if (success)
                {
                    // Navigate back to login page
                    await Shell.Current.GoToAsync("..");
                    await Shell.Current.DisplayAlert("Success", "Registration successful. Please login.", "OK");
                }
                else
                {
                    ErrorMessage = "Registration failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Registration failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            // Navigate back to login page
            await Shell.Current.GoToAsync("..");
        }
    }
}