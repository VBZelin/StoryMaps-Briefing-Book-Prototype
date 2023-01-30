using ArcGIS.StoryMaps.BriefingBook.Pages;

namespace ArcGIS.StoryMaps.BriefingBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            RegisterAllRoutes();

            ModifyUI();
        }

        private void RegisterAllRoutes()
        {
            Routing.RegisterRoute(nameof(PortalChooserPage), typeof(PortalChooserPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(GalleryPage), typeof(GalleryPage));
        }

        private void ModifyUI()
        {
        }
    }
}

