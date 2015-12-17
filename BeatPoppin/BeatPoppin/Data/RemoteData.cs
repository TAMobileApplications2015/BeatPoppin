namespace BeatPoppin.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Parse;
    using BeatPoppin.Data.Contracts;
    using BeatPoppin.Data.Models;
    using System.Linq;
    public class RemoteData
    {
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<User> Users => this.GetRemoteRepository<User>();

        public IRepository<HighScore> Highscores => this.GetRemoteRepository<HighScore>();

        public async Task AddScoreToUserAsync(HighScore score, User user)
        {
            score["parent"] = user;
            await score.SaveAsync();
        }

        public async Task<IEnumerable<ParseObject>> GetAllScoresForUserAsync(User user)
        {
            var query = ParseObject.GetQuery("Game").WhereEqualTo("parent", user);
            var result = await query.FindAsync();
            result.ToList();
            return result;
        }

        public async Task<HighScore> GetCurrentHighScoreAsync()
        {
            var query = new ParseQuery<HighScore>().OrderByDescending(h => h.Value);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        private IRepository<T> GetRemoteRepository<T>() where T : ParseObject
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, new RemoteGenericRepository<T>());
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
