using MinApp.Services;
using MinApp.ViewModels;
using System.Windows.Input;

namespace MinApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
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

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public LoginViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Login";

            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await GoToRegisterAsync());
            ForgotPasswordCommand = new Command(async () => await ForgotPasswordAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter email and password";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var success = await _apiService.LoginAsync(Email, Password);

                if (success)
                {
                    // Navigate to main page
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    ErrorMessage = "Invalid email or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync("RegisterPage");
        }

        private async Task ForgotPasswordAsync()
        {
            // Implement forgot password functionality
            await Shell.Current.DisplayAlert("Forgot Password", "This feature is not implemented yet.", "OK");
        }
    }
}