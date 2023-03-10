using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ArcGIS.StoryMaps.BriefingBook.Pages;
using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Services;
using Esri.ArcGISRuntime.Maui;

#if IOS && MACCATALYST
using SQLitePCL;
#endif

namespace ArcGIS.StoryMaps.BriefingBook
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

#if IOS && MACCATALYST
            raw.SetProvider(new SQLite3Provider_sqlite3());
#endif

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseArcGISRuntime();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<SQLiteDatabaseService>();
            mauiAppBuilder.Services.AddSingleton<ArcGISRuntimeService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<BriefingPageViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<PortalChooserPage>();
            mauiAppBuilder.Services.AddSingleton<SignInPage>();
            mauiAppBuilder.Services.AddSingleton<GalleryPage>();
            mauiAppBuilder.Services.AddTransient<BriefingPage>();

            return mauiAppBuilder;
        }
    }
}

