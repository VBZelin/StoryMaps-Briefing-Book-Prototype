using System;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    public class BriefingPageContentModel
    {
        required public int ColumnSplitPercentage { get; set; }
        required public int RowSplitPercentage { get; set; }
        required public int NumberLeftColumnBlocks { get; set; }
        required public int NumberRightColumnBlocks { get; set; }
        required public ContentModel[] PageContents { get; set; }
    }
}

