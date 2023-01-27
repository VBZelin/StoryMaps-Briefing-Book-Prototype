using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Services;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class PortalChooserPage : ContentPage
    {
        private PortalChooserPageViewModel _viewModel;

        private SQLiteDatabaseServer _sqlDatabaseServer;

        public PortalChooserPage(SQLiteDatabaseServer sqlDatabaseServer)
        {
            InitializeComponent();

            _sqlDatabaseServer = sqlDatabaseServer;

            _viewModel = new PortalChooserPageViewModel(_sqlDatabaseServer);

            BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.FilterPortalInfos();
        }

        private async void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
        {
            await _viewModel.FilterPortalInfos();
            await _viewModel.ValidateUrl();
        }

        private void OnEntryCompleted(System.Object sender, System.EventArgs e)
        {
        }

        private void OnDeleteSwipeItemInvoked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
