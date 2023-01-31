﻿using ArcGIS.StoryMaps.BriefingBook.Assets;
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
            Root.Title = SignInType == SignInType.OAuth ? "Sign in with ArcGIS Online" : "Sign in with ArcGIS Enterprise";

            switch (SignInType)
            {
                case SignInType.OAuth:
                    try
                    {
                        var serviceUrl = PortalUrl + "/sharing/rest";

                        ArcGISLoginPrompt.ServiceUrl = serviceUrl;

                        await ArcGISLoginPrompt.SetChallengeHandler();

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
                    await Shell.Current.DisplayAlert("Portal type", "This is a IWA portal!", "OK");

                    break;

                case SignInType.PKI:
                    await Shell.Current.DisplayAlert("Portal type", "This is a PKI portal!", "OK");

                    break;

                default:
                    return;
            }
        }
    }
}
