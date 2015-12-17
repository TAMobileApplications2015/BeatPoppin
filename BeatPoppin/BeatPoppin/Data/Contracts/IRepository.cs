namespace BeatPoppin.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);

        Task UpdateAsync(T item);

        Task DeleteAsync(T item);
    }
}
