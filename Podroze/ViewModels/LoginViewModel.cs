using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Podroze.Models;
using Podroze.Pages.App;

namespace Podroze.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using Podroze.Controllers;
using Podroze.Pages;
using Microsoft.WindowsAppSDK.Runtime.Packages;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthenticationController _authenticationController;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    [ObservableProperty] 
    private bool autologin;

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
            if (Autologin)
            {
                await SecureStorage.SetAsync("username", Username);
                await SecureStorage.SetAsync("password", Password);
            }

            Podroze.App.Current.MainPage = new NavigationPage(new SearchHotels());
            
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }

    private async void OnRegister()
    {
        var registerPage = Podroze.App.ServiceProvider.GetRequiredService<Register>();
        await Application.Current.MainPage.Navigation.PushAsync(registerPage);
    }
}