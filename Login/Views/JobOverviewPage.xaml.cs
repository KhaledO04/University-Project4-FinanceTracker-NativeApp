using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class JobOverviewPage : ContentPage
    {
        private readonly JobOverviewViewModel _viewModel;

        public JobOverviewPage(JobOverviewViewModel viewModel)
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