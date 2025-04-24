using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class PaycheckPage : ContentPage
    {
        private readonly PaycheckViewModel _viewModel;

        public PaycheckPage(PaycheckViewModel viewModel)
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