using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using Microsoft.UI.Xaml.Controls;
using Podroze.Controllers;
using Podroze.Pages;
using Podroze.Pages.Views;
using Podroze.ViewModels;
using WinRT;

namespace Podroze
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            await Logout();
        }

        private async Task Logout()
        {
            // Usunięcie danych logowania z SecureStorage
            var AutController = App.ServiceProvider.GetService<IAuthenticationController>();
            await AutController.LogOut();

            // Nawigacja do strony logowania
            var loginPage = App.ServiceProvider.GetRequiredService<Login>();
            Application.Current.MainPage = loginPage;
        }
    }
}
