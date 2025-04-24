using MinApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MinApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // S�t ViewModel som BindingContext for data-binding
    }
}
