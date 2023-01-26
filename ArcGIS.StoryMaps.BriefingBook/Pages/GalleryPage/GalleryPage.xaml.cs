using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(AGSPortal), "portal")]
    public partial class GalleryPage : ContentPage
    {
        ArcGISPortal _portal;
        public ArcGISPortal AGSPortal
        {
            get => _portal;
            set
            {
                _portal = value;               
                _galleryViewModel.portal= value;
                _galleryViewModel.SearchPublicItems();

                OnPropertyChanged();
            }
        }
        GalleryPageViewModel _galleryViewModel;

        int _gridcolumns = 2;
       
        public int GridColumns
        {
            get => _gridcolumns;
            set
            {
                _gridcolumns = value;                
                OnPropertyChanged();
            }
        }



        public GalleryPage()
        {
            InitializeComponent();
            _galleryViewModel = new GalleryPageViewModel();
            this.BindingContext = _galleryViewModel;
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            GridColumns = (int)(mainDisplayInfo.Width / 400);
        }
    }
}
