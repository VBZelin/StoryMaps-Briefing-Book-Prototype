using System;
using ArcGIS.StoryMaps.BriefingBook.Helpers;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;
using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class ArcGISRuntimeService
    {
        public PortalManager PortalManager { get; private set; }

        public ArcGISRuntimeService() { }

        public async Task<ArcGISPortal> GetPortalIfUrlIsValid(string portalUrl)
        {
            try
            {
                if (portalUrl.EndsWith("/"))
                    portalUrl += "sharing/rest";
                else
                    portalUrl += "/sharing/rest";

                ArcGISPortal securedPortal = await ArcGISPortal.CreateAsync(new Uri(portalUrl));

                return securedPortal;
            }
            catch (Exception e)
            {
                Utility.DebugLogObject(e);

                return null;
            }
        }
    }
}

