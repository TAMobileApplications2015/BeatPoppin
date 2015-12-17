namespace BeatPoppin.Data
{
    using System;
    using System.IO;
    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;
    using Windows.Storage;
    using Models;

    public class LocalDb
    {
        public async void InitAsync()
        {
            var connection = this.GetConnection();
            await connection.CreateTableAsync<GameScore>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            var dbFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                    new SQLiteConnectionWithLock(
                        new SQLitePlatformWinRT(),
                        new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }
    }
}