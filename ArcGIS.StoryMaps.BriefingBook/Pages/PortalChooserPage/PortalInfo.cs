using System;
using SQLite;

namespace ArcGIS.StoryMaps.BriefingBook.Models
{
    [Table(nameof(PortalInfo))]
    public class PortalInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime UnixTime { get; set; }
        public string Json { get; set; }
    }
}

