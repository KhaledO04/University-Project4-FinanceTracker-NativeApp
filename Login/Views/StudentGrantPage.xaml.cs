using MinApp.ViewModels;

namespace MinApp.Views
{
    public partial class StudentGrantPage : ContentPage
    {
        private readonly StudentGrantViewModel _viewModel;

        public StudentGrantPage(StudentGrantViewModel viewModel)
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