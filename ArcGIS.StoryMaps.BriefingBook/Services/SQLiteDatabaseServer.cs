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

            var result = await Database.CreateTableAsync<PortalInfo>();

            // Add tables in here
        }

        public async Task<List<PortalInfo>> GetPortalInfosSortedByUnixTimeAsync(string inputUrl)
        {
            await Init();

            var query = $"SELECT * FROM PortalInfo WHERE Url LIKE '%{inputUrl}%' ORDER BY UnixTime DESC";

            return await Database.QueryAsync<PortalInfo>(query);
        }

        public async Task<int> AddPortalInfoAsync(PortalInfo portalInfo)
        {
            await Init();

            if (portalInfo.Url != "")
                return await Database.UpdateAsync(portalInfo);
            else
                return await Database.InsertAsync(portalInfo);
        }

        public async Task<int> DeletePortalInfoAsync(PortalInfo portalInfo)
        {
            await Init();

            return await Database.DeleteAsync(portalInfo);
        }
    }
}

