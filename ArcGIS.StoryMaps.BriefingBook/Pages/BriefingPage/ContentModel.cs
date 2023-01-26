using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class ContentModel
    {
        public int ColumnPosition { get; set; }
        public int RowPosition { get; set; }
        public virtual string ContentType { get; set; } = "DEFAULT CONTENT";
    }
}

