using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class LogHoursPage : ContentPage
    {
        private readonly LogHoursViewModel _viewModel;

        public LogHoursPage(LogHoursViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}