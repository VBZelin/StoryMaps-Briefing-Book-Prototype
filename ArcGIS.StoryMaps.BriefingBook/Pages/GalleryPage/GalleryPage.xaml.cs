using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(ArcGISPortal), nameof(ArcGISPortal))]
    public partial class GalleryPage : ContentPage
    {
        private ArcGISPortal _portal;
        public ArcGISPortal ArcGISPortal
        {
            get => _portal;

            set
            {
                if (value != _portal)
                {
                    _portal = value;

                    _galleryViewModel.portal = value;
                    _galleryViewModel.SearchPublicItems();

                    OnPropertyChanged();
                }
            }
        }

        private int _gridColumns = 2;
        public int GridColumns
        {
            get => _gridColumns;

            set
            {
                if (value != _gridColumns)
                {
                    _gridColumns = value;

                    OnPropertyChanged();
                }
            }
        }

        private GalleryPageViewModel _galleryViewModel;

        public GalleryPage()
        {
            InitializeComponent();

            _galleryViewModel = new GalleryPageViewModel();
            BindingContext = _galleryViewModel;

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            GridColumns = (int)(mainDisplayInfo.Width / 400);
        }
    }
}
