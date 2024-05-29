using Podroze.Pages;

namespace Podroze.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using Podroze.Controllers;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthenticationController _authenticationController;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public LoginViewModel(IAuthenticationController AuthenticationController)
    {
        _authenticationController = AuthenticationController;

        LoginCommand = new RelayCommand(OnLogin);
        RegisterCommand = new RelayCommand(OnRegister);
    }

    private async void OnLogin()
    {
        // Implement your authentication logic here
        bool isAuthenticated = _authenticationController.AuthenticateUser(Username, Password);

        if (isAuthenticated)
        {
            Application.Current.MainPage = new MainPage();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }

    private async void OnRegister()
    {
        var registerPage = App.ServiceProvider.GetRequiredService<Register>();
        await Application.Current.MainPage.Navigation.PushAsync(registerPage);
    }
}