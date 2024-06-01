using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Proyecto_final.View;
using Bogus;
using Microsoft.Maui.Handlers;

namespace Proyecto_final
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("can-dog-tfb.ttf", "dog");
                fonts.AddFont("kitty-cats-tfb.ttf", "cat");
                fonts.AddFont("SansFierro.ttf", "sansfierro");
                fonts.AddFont("SansFierro-Texture.ttf", "sansfierro-textura");
                fonts.AddFont("Saudagar.ttf", "saudagar");
            });
            builder.UseMauiCommunityToolkit();
            builder.Services.AddSingleton<DiceBear>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.ConfigureMauiHandlers(handlers =>
            {
#if WINDOWS
                SwitchHandler.Mapper.AppendToMapping("Custom", (h, _) =>
                {
                    h.PlatformView.OffContent = string.Empty;
                    h.PlatformView.OnContent = string.Empty;

                    h.PlatformView.MinWidth = 0;
                });
#endif
            });
            return builder.Build();
        }
    }
}