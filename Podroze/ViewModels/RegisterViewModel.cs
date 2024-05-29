using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Podroze.Controllers;
using System;
using Podroze.Models;
using Podroze.Pages;

namespace Podroze.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string firstName;

    [ObservableProperty]
    private string lastName;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string passwordConfirm;

    public ICommand RegisterCommand { get; }
    private readonly TravelDBContext _context;
    private readonly IAuthenticationController _authenticationController;
    

    public RegisterViewModel(TravelDBContext context, IAuthenticationController _authentication)
    {
        RegisterCommand = new RelayCommand(OnRegister);
        _context = context;
        _authenticationController = _authentication;
    }

    private async void OnRegister()
    {
        if (_authenticationController.GetUserByUsername(username) != null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ta nazwa użytkownika jest już zajęta", "OK");
            return;
        }


        if (_authenticationController.GetUserByEmail(email) != null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ten Adres Email jest już zajęty", "OK");
            return;
        }

        if (password != passwordConfirm)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Podane hasła się różnią", "OK");

            await Application.Current.MainPage.DisplayAlert("Error", password, "OK");

            await Application.Current.MainPage.DisplayAlert("Error", passwordConfirm, "OK");
            return;
        }

        int registered = await RegisterUser();

        if (registered == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Wystąpił błąd podczas rejestracji", "OK");
            return;
        }
        else
        {
            var loginPage = App.ServiceProvider.GetRequiredService<Login>();
           await Application.Current.MainPage.Navigation.PushAsync(loginPage);
        }

    }

    private async Task<int> RegisterUser()
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            FirtsName = firstName,
            LastName = LastName,
            Email = email,
            Username = username,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        int result = _context.SaveChanges();

        return result;
    }
}