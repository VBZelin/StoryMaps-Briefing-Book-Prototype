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
            Root.Title = "Sign in with ArcGIS Online";

            switch (SignInType)
            {
                case SignInType.OAuth:
                    try
                    {
                        var serviceUrl = PortalUrl + "/sharing/rest";

                        ArcGISLoginPrompt.ServiceUrl = serviceUrl;
                        ArcGISLoginPrompt.SetChallengeHandler();

                        ArcGISPortal securedPortal = await ArcGISPortal.CreateAsync(new Uri(serviceUrl), true);

                        if (securedPortal.User is not null)
                        {
                            _arcGISRuntimeService.ArcGISPortalManager.SetSignedInPortal(securedPortal);

                            await Shell.Current.GoToAsync($"{nameof(GalleryPage)}");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Error", "Sign in failed", "OK");
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                    catch (Exception e)
                    {
                        await Shell.Current.DisplayAlert("Error", e.ToString(), "OK");
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
    }
}
