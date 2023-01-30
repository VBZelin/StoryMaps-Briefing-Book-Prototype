using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class ContentModel
    {
        required public int ColumnPosition { get; set; }
        required public int RowPosition { get; set; }
        public virtual string ContentType { get; set; } = "Refresh to reload data";
    }
}

