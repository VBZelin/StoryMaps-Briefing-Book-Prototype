using System;
using System.Windows.Input;
using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Services;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    public class GalleryPageViewModel
    {
        public ICommand BackButtonClickedCommand { get; private set; }

        private ArcGISRuntimeService _arcGISRuntimeService;

        public GalleryPageViewModel(ArcGISRuntimeService arcGISRuntimeService)
        {
            _arcGISRuntimeService = arcGISRuntimeService;

            Init();
        }

        private void Init()
        {
            BackButtonClickedCommand = new Command(async () =>
            {
                var result = await Shell.Current.DisplayAlert(
                    StringSources.SIGN_OUT_CAP,
                    StringSources.SIGN_OUT_DESCRIPTION,
                    StringSources.SIGN_OUT_CAP,
                    StringSources.CANCEL_CAP);

                if (result)
                {
                    await _arcGISRuntimeService.RemoveAndRevokeAllCredentials();

                    _arcGISRuntimeService.ArcGISPortalManager.ResetSignInPortal();

                    await Shell.Current.GoToAsync("../../..");
                }
            });
        }
    }
}

