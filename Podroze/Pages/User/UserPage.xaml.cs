using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Podroze.Pages.User;

public partial class UserPage : ContentPage
{
    public IList<IView> BasePageContent => BaseContent.Children;

	public UserPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        TapGestureRecognizer HouseButtonRecognizer = new();
        HouseButtonRecognizer.Tapped += async (_, e) =>
        {
            await Navigation.PopToRootAsync();
        };

        TapGestureRecognizer DashboardButtonRecognizer = new();
        DashboardButtonRecognizer.Tapped += async (_, e) =>
        {
            await Navigation.PushAsync(new Podroze.Pages.User.Dashboard(), true);
        };

        TapGestureRecognizer UserDetailsButtonRecognizer = new();
        UserDetailsButtonRecognizer.Tapped += async (_, e) =>
        {
            await Navigation.PushAsync(new Podroze.Pages.User.UserDetails(), true);
        };

        DashboardButton.GestureRecognizers.Add(DashboardButtonRecognizer);
        UserDetailsButton.GestureRecognizers.Add(UserDetailsButtonRecognizer);
    }
}