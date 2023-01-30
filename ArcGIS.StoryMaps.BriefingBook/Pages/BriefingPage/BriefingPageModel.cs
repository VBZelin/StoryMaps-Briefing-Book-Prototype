using System;

using ArcGIS.StoryMaps.BriefingBook.Assets;


namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageModel
    {
        required public string PageTitle { get; set; }
        required public int PageNumber { get; set; }
        public string PageHeaderIcon { get; set; }
        public ClassificationType ClassificationType { get; set; }
    }
}


