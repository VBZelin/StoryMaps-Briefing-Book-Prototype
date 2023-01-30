using System;
using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime
{
    public class PortalManager
    {
        public PortalManager()
        {
        }

        public async Task<ArcGISPortal> GetPortalIfUrlIsValid(string portalUrl)
        {
            try
            {
                var sericeUrl = portalUrl + (portalUrl.EndsWith("/") ? "sharing/rest" : "/sharing/rest");

                ArcGISPortal securedPortal = await ArcGISPortal.CreateAsync(new Uri(sericeUrl));

                return securedPortal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}

