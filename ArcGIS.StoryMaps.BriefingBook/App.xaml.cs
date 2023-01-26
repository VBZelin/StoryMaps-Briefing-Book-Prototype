﻿using ArcGIS.StoryMaps.BriefingBook.Pages;

//  #if ANDROID
//  using Android.Content.Res;
//  using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//  #endif

namespace ArcGIS.StoryMaps.BriefingBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            RegisterAllRoutes();

            // ModifyUI();
        }

        private void RegisterAllRoutes()
        {
            Routing.RegisterRoute(nameof(PortalChooserPage), typeof(PortalChooserPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(GalleryPage), typeof(GalleryPage));
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute(nameof(BriefingPage), typeof(BriefingPage));
        }

        private void ModifyUI()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderLine", (handler, view) =>
            {
#if ANDROID
                // handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
            });
        }
    }
}

