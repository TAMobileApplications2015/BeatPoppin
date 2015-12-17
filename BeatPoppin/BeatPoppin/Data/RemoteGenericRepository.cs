namespace BeatPoppin.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Parse;
    using BeatPoppin.Data.Contracts;

    public class RemoteGenericRepository<T> : IRepository<T> where T : ParseObject
    {
        public async Task AddAsync(T item)
        {
            await item.SaveAsync();
        }

        public async Task DeleteAsync(T item)
        {
            await item.DeleteAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await new ParseQuery<T>().FindAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var result = await new ParseQuery<T>().GetAsync(id.ToString());
            return result;
        }

        public async Task<T> FetchFromServerAsync(T item)
        {
            var result = await item.FetchAsync();
            return result;
        }

        public async Task UpdateAsync(T item)
        {
            await item.SaveAsync();
        }
    }
}
