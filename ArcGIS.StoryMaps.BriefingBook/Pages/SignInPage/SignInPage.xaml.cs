using ArcGIS.StoryMaps.BriefingBook.Assets;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(PortalUrl), nameof(PortalUrl))]
    [QueryProperty(nameof(SignInType), nameof(SignInType))]
    public partial class SignInPage : ContentPage
    {
        public Uri PortalUrl { get; set; }

        public SignInType SignInType { get; set; }

        public SignInPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Init();
        }

        public void Init() { }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}");
        }
    }
}
