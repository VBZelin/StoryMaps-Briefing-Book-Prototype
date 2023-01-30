using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Models;
using ArcGIS.StoryMaps.BriefingBook.Services;
using ArcGIS.StoryMaps.BriefingBook.ViewModels;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class GalleryPage : ContentPage
    {
        private GalleryPageViewModel _viewModel;
        private ArcGISRuntimeService _arcGISRuntimeService;

        public GalleryPage(ArcGISRuntimeService arcGISRuntimeService)
        {
            InitializeComponent();

            _arcGISRuntimeService = arcGISRuntimeService;

            _viewModel = new GalleryPageViewModel(_arcGISRuntimeService);

            BindingContext = _viewModel;
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            ContentModel[] pageContents = new ContentModel[]
            {
                new TextContentModel { ColumnPosition = 0, RowPosition = 0 },
                new MapContentModel { ColumnPosition = 0, RowPosition = 1 },
                new MediaContentModel { ColumnPosition = 1, RowPosition = 0 },
                new TextContentModel { ColumnPosition = 1, RowPosition = 1 },
            };

            BriefingPageContentModel briefingPageContentModel = new()
            {
                ColumnSplitPercentage = 50,
                RowSplitPercentage = 50,
                NumberLeftColumnBlocks = 2,
                NumberRightColumnBlocks = 1,
                PageContents = pageContents,
            };

            BriefingPageModel briefingPageModel = new()
            {
                PageTitle = "Briefing Page 1",
                PageNumber = 1,
                ClassificationType = ClassificationType.Unclassified
            };

            Dictionary<string, object> pageParameters = new()
            {
                ["BriefingPageContentModel"] = briefingPageContentModel,
                ["BriefingPageModel"] = briefingPageModel
            };

            await Shell.Current.GoToAsync($"/{nameof(BriefingPage)}", true, pageParameters);
        }
    }
}
