using System;
using System.Text.RegularExpressions;

#if ANDROID
using Newtonsoft.Json;
#endif

namespace ArcGIS.StoryMaps.BriefingBook.Helpers
{
    public static class Utility
    {
        // Convert url to portal url starts with 'https://'
        public static string GetRealPortalUrl(string text)
        {
            var temp = Uri.EscapeDataString(text.ToLower());

            var url = Uri.UnescapeDataString(temp);

            if (!url.Contains("https://") && !url.Contains("http://"))
                url = "https://" + url;

            if (url.Contains("http://"))
                url = url.Replace("http://", "https://");

            return url;
        }

        // Check if it is a valid url
        public static bool IsUrl(string text)
        {
            Regex regex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

            MatchCollection matchCollection = regex.Matches(text);

            return matchCollection.Count > 0;
        }

        // Convert datetime to UNIX string
        public static string ToUnixTime(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime.ToUniversalTime());

            return dateTimeOffset.ToUnixTimeSeconds().ToString();
        }

        // Debug log object
        public static void DebugLogObject(object _object)
        {
#if ANDROID
            var text = JsonConvert.SerializeObject(_object, Formatting.Indented);

            Console.WriteLine(text);
#endif
        }
    }
}

