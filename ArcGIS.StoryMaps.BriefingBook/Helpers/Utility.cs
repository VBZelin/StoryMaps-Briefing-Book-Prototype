using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ArcGIS.StoryMaps.BriefingBook.Helpers
{
    public static class Utility
    {
        /// <summary>
        /// Convert url to portal url starts with 'https://'
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        /// (string)PortalUrl
        /// </returns>
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

        /// <summary>
        /// Check if it is a valid url
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        /// (bool)IsUrl
        /// </returns>
        public static bool IsUrl(string text)
        {
            Regex regex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

            MatchCollection matchCollection = regex.Matches(text);

            return matchCollection.Count > 0;
        }

        /// <summary>
        /// Convert datetime to UNIX string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>
        /// (string)UnixTime
        /// </returns>
        public static string ToUnixTime(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime.ToUniversalTime());

            return dateTimeOffset.ToUnixTimeSeconds().ToString();
        }

        /// <summary>
        /// Debug log object
        /// </summary>
        /// <param name="_object"></param>
        public static void DebugLogObject(object _object)
        {
            var text = JsonConvert.SerializeObject(_object, Formatting.Indented);

            Console.WriteLine(text);
        }
    }
}

