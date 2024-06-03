using CommunityToolkit.Maui.Views;

namespace Podroze.Pages.Views;

public partial class HeaderBar : ContentView
{
	public HeaderBar()
	{
		InitializeComponent();
    }

    private async void OnAvatarTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Podroze.Pages.User.Dashboard());
    }

}