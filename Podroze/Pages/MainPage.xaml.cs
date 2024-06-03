using Podroze.Controllers;
using Podroze.Pages;
using System.Reflection;
using System.Text.Json;

namespace Podroze
{
    public partial class MainPage : ContentPage
    {
        public IList<IView> BasePageContent => ContentWrapper.Children;
        public MainPage()
        {
            InitializeComponent();
        }

    }

}
