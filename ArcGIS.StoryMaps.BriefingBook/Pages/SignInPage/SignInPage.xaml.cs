using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            ArcGISPortal arcgisPortal = await ArcGISPortal.CreateAsync();
            var navigationParameter = new Dictionary<string, object>
              {
                 { "portal",arcgisPortal}

              };

            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}",navigationParameter);
        }
    }
}
