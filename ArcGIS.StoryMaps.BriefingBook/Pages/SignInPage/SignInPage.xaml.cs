using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Services;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(PortalUrl), nameof(PortalUrl))]
    public partial class SignInPage : ContentPage
    {
        public string PortalUrl { get; set; }

        private SignInPageViewModel _viewModel;

        private ArcGISRuntimeService _arcGISRuntimeService;

        public SignInPage(ArcGISRuntimeService arcGISRuntimeService)
        {
            InitializeComponent();

            _arcGISRuntimeService = arcGISRuntimeService;

            _viewModel = new SignInPageViewModel(_arcGISRuntimeService);

            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Init();
        }

        public void Init()
        {

        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}");
        }
    }
}
