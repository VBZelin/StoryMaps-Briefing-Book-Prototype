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
    }
}
