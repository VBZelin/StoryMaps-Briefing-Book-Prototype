using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(SecuredPortal), nameof(SecuredPortal))]
    public partial class SignInPage : ContentPage
    {
        public ArcGISPortal SecuredPortal;

        public SignInPage()
        {
            InitializeComponent();
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}");
        }
    }
}
