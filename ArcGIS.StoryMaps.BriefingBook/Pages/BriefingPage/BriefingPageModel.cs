using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageModel
    {
        public enum ClassificationTypes { Unclassified, Classified }
        required public string PageTitle { get; set; }
        required public int PageNumber { get; set; }
        public string PageHeaderIcon { get; set; }
        public ClassificationTypes Classification { get; set; }
    }
}


