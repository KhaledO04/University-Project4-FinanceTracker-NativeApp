using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class AddHoursPage : ContentPage
    {
        private readonly AddHoursViewModel _viewModel;

        public AddHoursPage(AddHoursViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
    }
}