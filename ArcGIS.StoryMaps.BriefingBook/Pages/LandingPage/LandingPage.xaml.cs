namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void OnSignInButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(PortalChooserPage)}");
        }
    }
}