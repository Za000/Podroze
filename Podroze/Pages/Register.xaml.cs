using Podroze.Controllers;
using Podroze.ViewModels;

namespace Podroze.Pages;

public partial class Register : ContentPage
{
	public Register(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}