using Podroze.Controllers;
using Podroze.Pages;
using System.Reflection;
using System.Text.Json;

namespace Podroze
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
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
            Application.Current.MainPage = new NavigationPage(loginPage);
        }
    }

}
