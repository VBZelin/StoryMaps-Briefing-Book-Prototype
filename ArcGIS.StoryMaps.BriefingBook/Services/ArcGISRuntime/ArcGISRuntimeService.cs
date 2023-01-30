using System;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class ArcGISRuntimeService
    {
        public PortalManager PortalManager { get; private set; }

        public ArcGISRuntimeService()
        {
            PortalManager = new PortalManager();
        }
    }
}

