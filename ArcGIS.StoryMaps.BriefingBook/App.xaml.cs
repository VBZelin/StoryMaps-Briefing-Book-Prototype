namespace ArcGIS.StoryMaps.BriefingBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            RegisterAllRoutes();
        }

        private void RegisterAllRoutes()
        {

        }
    }
}

