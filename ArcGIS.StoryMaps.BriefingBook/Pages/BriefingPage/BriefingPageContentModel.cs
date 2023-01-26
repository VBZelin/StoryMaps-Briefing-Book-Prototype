using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageContentModel
    {
        public int ColumnSplit { get; set; }
        public int RowSplit { get; set; }
        public int NumberLeftColumnBlocks { get; set; }
        public int NumberRightColumnBlocks { get; set; }
        public ContentModel[] PageContents { get; set; }
    }
}

