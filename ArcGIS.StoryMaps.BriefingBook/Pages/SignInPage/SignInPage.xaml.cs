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
            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}");
        }
    }
}
