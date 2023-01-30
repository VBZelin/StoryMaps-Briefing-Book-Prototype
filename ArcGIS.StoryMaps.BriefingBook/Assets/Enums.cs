using System;

namespace ArcGIS.StoryMaps.BriefingBook.Assets
{
    public enum SignInType : int
    {
        Unknown = -1,
        OAuth = 0,
        IWA = 1,
        PKI = 2
    }

    public enum ClassificationType : int
    {
        Unknown = -1,
        Unclassified = 0,
        Classified = 1
    }
}

