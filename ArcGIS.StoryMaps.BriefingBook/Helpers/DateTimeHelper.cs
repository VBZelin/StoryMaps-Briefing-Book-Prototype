using System;

namespace ArcGIS.StoryMaps.BriefingBook.Helpers
{
    public static class DateTimeHelper
    {
        // Convert datetime to UNIX time
        public static long ToUnixTime(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime.ToUniversalTime());

            return (long)dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}

