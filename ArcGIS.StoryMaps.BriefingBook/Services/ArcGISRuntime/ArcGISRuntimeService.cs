using System;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class ArcGISRuntimeService
    {
        public ArcGISPortalManager ArcGISPortalManager { get; private set; }

        public ArcGISRuntimeService()
        {
            ArcGISPortalManager = new ArcGISPortalManager();
        }
    }
}

