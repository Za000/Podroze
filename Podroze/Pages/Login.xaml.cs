using Podroze.ViewModels;

namespace Podroze.Pages;

public partial class Login : ContentPage
{

    public Login(LoginViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}