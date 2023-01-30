using System;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;
using Esri.ArcGISRuntime.Security;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class ArcGISRuntimeService
    {
        public ArcGISPortalManager ArcGISPortalManager { get; private set; }

        public ArcGISRuntimeService()
        {
            ArcGISPortalManager = new ArcGISPortalManager();
        }

        public async Task RemoveAndRevokeAllCredentials()
        {
            await AuthenticationManager.Current.RemoveAndRevokeAllCredentialsAsync();
        }
    }
}

