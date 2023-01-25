namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class PortalChooserPage : ContentPage
    {
        public PortalChooserPage()
        {
            InitializeComponent();
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            // TODO: add logic in here

            await Shell.Current.GoToAsync($"/{nameof(SignInPage)}");
        }

        private void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
        {
        }

        private void OnEntryCompleted(System.Object sender, System.EventArgs e)
        {
        }

        private void OnDeleteSwipeItemInvoked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
