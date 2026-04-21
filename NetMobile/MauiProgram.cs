using Microsoft.Extensions.Logging;
using NetMobile.Pages;
using NetMobile.Services;

namespace NetMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servicios
            builder.Services.AddSingleton<ApiService>();

            // Páginas (para poder inyectar ApiService por constructor)
            builder.Services.AddTransient<PaisesPage>();
            builder.Services.AddTransient<ConfederacionesPage>();
            builder.Services.AddTransient<DeportesPage>();
            builder.Services.AddTransient<PaisFormPage>();
            builder.Services.AddTransient<ConfederacionFormPage>();
            builder.Services.AddTransient<DeporteFormPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
