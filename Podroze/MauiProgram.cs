using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Podroze.Controllers;
using Podroze.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Podroze.Pages;
using IeuanWalker.Maui.StateButton;
using Microsoft.Maui.Controls.Hosting;

namespace Podroze
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FaBrands400");
                    fonts.AddFont("fa-regular-400.ttf", "FaRegular400");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid900");
                })
                .UseMauiCommunityToolkitMaps("ApTmW0bvIx-u-_DJpg8KJ3qvRqKevv_XOtvZGHgvEw0Ga2uJEA_eKik4UbVROVY6");
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
