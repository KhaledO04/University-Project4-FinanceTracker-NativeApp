using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class HolidayPayPage : ContentPage
    {
        private readonly HolidayPayViewModel _viewModel;

        public HolidayPayPage(HolidayPayViewModel viewModel)
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