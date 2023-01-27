﻿using System;
using Esri.ArcGISRuntime.Portal;
using ArcGIS.StoryMaps.BriefingBook.Helpers;
using ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class ArcGISRuntimeService
    {
        public PortalManager PortalManager { private set; get; }

        public ArcGISRuntimeService() { }

        public async Task<ArcGISPortal> GetPortalIfUrlIsValid(string portalUrl)
        {
            try
            {
                ArcGISPortal arcGISPortal = await ArcGISPortal.CreateAsync(new Uri(portalUrl));

                return arcGISPortal;
            }
            catch (Exception e)
            {
                Utility.DebugLogObject(e);

                return null;
            }
        }
    }
}

