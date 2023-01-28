using System;
using SQLite;
using ArcGIS.StoryMaps.BriefingBook.Models;

namespace ArcGIS.StoryMaps.BriefingBook.Services
{
    public class SQLiteDatabaseService
    {
        SQLiteAsyncConnection Database;

        public const string FileName = "ArcGIS_StoryMaps_BriefingBook_SQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public SQLiteDatabaseService() { }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(DatabasePath, Flags);

            var result = await Database.CreateTableAsync<PortalInfoItem>();

            // Add tables in here
        }

        public async Task<List<PortalInfoItem>> GetPortalInfoItemsSortedByUnixTimeAsync(string inputUrl)
        {
            await Init();

            var query = $"SELECT * FROM PortalInfoItem WHERE Url LIKE '%{inputUrl}%' ORDER BY UnixTime DESC";

            return await Database.QueryAsync<PortalInfoItem>(query);
        }

        public async Task<PortalInfoItem> GetPortalInfoItemByUrlAsync(string url)
        {
            await Init();

            return await Database.Table<PortalInfoItem>().Where(item => item.Url == url).FirstOrDefaultAsync();
        }

        public async Task<int> AddPortalInfoItemAsync(PortalInfoItem portalInfoItem)
        {
            await Init();

            var item = await GetPortalInfoItemByUrlAsync(portalInfoItem.Url);

            if (item is not null)
                return await Database.UpdateAsync(portalInfoItem);

            return await Database.InsertAsync(portalInfoItem);
        }

        public async Task<int> DeletePortalInfoItemAsync(PortalInfoItem portalInfoItem)
        {
            await Init();

            return await Database.DeleteAsync(portalInfoItem);
        }
    }
}

