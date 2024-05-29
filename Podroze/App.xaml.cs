using Microsoft.EntityFrameworkCore;
using Podroze.Controllers;
using Podroze.Pages;
using Podroze.ViewModels;

namespace Podroze
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
            var mainPage = ServiceProvider.GetRequiredService<Login>();

            MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            base.OnStart();

            AttemptAutoLogin();
        }

        private async Task AttemptAutoLogin()
        {
            var AuthController = ServiceProvider.GetService<IAuthenticationController>();
            bool LoggedIn = await AuthController.AutoLoginAuthentication();

            if (LoggedIn)
            {
                Application.Current.MainPage = new AppShell();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            AppSettings settings = new AppSettings();
            services.AddDbContext<TravelDBContext>(options =>
                options.UseSqlServer(settings.dbconnect));

            services.AddSingleton<IAuthenticationController, AuthenticationController>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<Login>();
            services.AddTransient<Register>();
        }
    }
}
