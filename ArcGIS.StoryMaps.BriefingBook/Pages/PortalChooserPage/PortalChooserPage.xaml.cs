using ArcGIS.StoryMaps.BriefingBook.Models;
using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Services;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class PortalChooserPage : ContentPage
    {
        private PortalChooserPageViewModel _viewModel;

        private SQLiteDatabaseService _sqlDatabaseSevrvice;
        private ArcGISRuntimeService _arcGISRuntimeService;

        public PortalChooserPage(SQLiteDatabaseService sqlDatabaseService, ArcGISRuntimeService arcGISRuntimeService)
        {
            InitializeComponent();

            _sqlDatabaseSevrvice = sqlDatabaseService;
            _arcGISRuntimeService = arcGISRuntimeService;

            _viewModel = new PortalChooserPageViewModel(_sqlDatabaseSevrvice, _arcGISRuntimeService);

            BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.FilterPortalInfoItems();
        }

        private async void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
        {
            await _viewModel.FilterPortalInfoItems();
            await _viewModel.ValidateUrl();
        }

        private void OnEntryCompleted(System.Object sender, System.EventArgs e)
        {
        }

        private async void OnDeleteSwipeItemInvoked(System.Object sender, System.EventArgs e)
        {
            var item = sender as SwipeItem;

            var portalInfoItem = item.BindingContext as PortalInfoItem;

            await _viewModel.DeletePortalInfoItem(portalInfoItem);
        }
    }
}
