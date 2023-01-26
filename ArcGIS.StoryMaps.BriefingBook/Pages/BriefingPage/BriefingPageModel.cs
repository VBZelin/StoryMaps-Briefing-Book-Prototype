using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageModel
    {
        public enum ClassificationTypes { Unclassified, Classified }
        public string PageTitle { get; set; }
        public int PageNumber { get; set; }
        public string PageHeaderIcon { get; set; }
        public ClassificationTypes Classification { get; set; }
    }
}


