using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageContentModel
    {
        required public int ColumnSplit { get; set; }
        required public int RowSplit { get; set; }
        required public int NumberLeftColumnBlocks { get; set; }
        required public int NumberRightColumnBlocks { get; set; }
        required public ContentModel[] PageContents { get; set; }
    }
}

