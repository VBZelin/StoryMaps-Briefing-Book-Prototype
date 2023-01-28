using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Services;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;
using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    [QueryProperty(nameof(PortalUrl), nameof(PortalUrl))]
    [QueryProperty(nameof(SignInType), nameof(SignInType))]
    public partial class SignInPage : ContentPage
    {
        public string PortalUrl { get; set; }

        public SignInType SignInType { get; set; }

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
            _ = DisplaySignInUI();
        }

        public async Task DisplaySignInUI()
        {
            switch (SignInType)
            {
                case SignInType.OAuth:
                    try
                    {
                        ArcGISLoginPrompt.SetChallengeHandler();

                        ArcGISPortal arcgisPortal = await ArcGISPortal.CreateAsync(new Uri(PortalUrl), true);
                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "OK");
                    }

                    break;

                case SignInType.IWA:
                    break;

                case SignInType.PKI:
                    break;

                default:
                    return;
            }
        }

        private async void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(GalleryPage)}");
        }
    }
}
