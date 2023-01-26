using ArcGIS.StoryMaps.BriefingBook.Models;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            InitializeComponent();
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            ContentModel[] pageContents = new ContentModel[]
            {
                new TextContentModel { ColumnPosition = 0, RowPosition = 0 },
                new MapContentModel { ColumnPosition = 0, RowPosition = 1 },
                new MediaContentModel { ColumnPosition = 1, RowPosition = 0 },
            };

            BriefingPageContentModel briefingPageContentModel = new()
            {
                ColumnSplit = 50,
                RowSplit = 50,
                NumberLeftColumnBlocks = 2,
                NumberRightColumnBlocks = 1,
                PageContents = pageContents,
            };

            BriefingPageModel briefingPageModel = new()
            {
                PageTitle = "Briefing Page 1",
                PageNumber = 1,
                Classification = BriefingPageModel.ClassificationTypes.Classified
            };

            Dictionary<string, object> pageParameters = new()
            {
                {"BriefingPageContentModel", briefingPageContentModel },
                {"BriefingPageModel", briefingPageModel }
            };

            await Shell.Current.GoToAsync($"/{nameof(BriefingPage)}", true, pageParameters);
        }
    }
}
