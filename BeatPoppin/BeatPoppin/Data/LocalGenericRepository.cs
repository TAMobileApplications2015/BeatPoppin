namespace BeatPoppin.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SQLite.Net.Async;
    using Contracts;

    public class LocalGenericRepository<T> : IRepository<T> where T : class
    {
        private LocalDb db;
        private SQLiteAsyncConnection dbConnection;

        public LocalGenericRepository(LocalDb db)
        {
            this.db = db;
            this.dbConnection = this.db.GetConnection();
        }

        public async Task AddAsync(T item)
        {
            await this.dbConnection.InsertAsync(item);
        }

        public async Task DeleteAsync(T item)
        {
            await this.dbConnection.DeleteAsync(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await this.dbConnection.Table<T>().ToListAsync();

            return result;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var result = await this.dbConnection.FindAsync<T>(id);

            return result;
        }

        public async Task UpdateAsync(T item)
        {
            await this.dbConnection.UpdateAsync(item);
        }
    }
}
