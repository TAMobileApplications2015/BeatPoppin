namespace BeatPoppin.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    public class LocalData
    {
        private LocalDb context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public LocalData(LocalDb context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of LocalDb is required to use this repository.", nameof(context));
            }

            this.context = context;
            this.context.InitAsync();
        }

        public IRepository<GameScore> GameScores => this.GetLocalRepository<GameScore>();

        public async Task<GameScore> GetCurrentHighScoreAsync()
        {
            var score = (await this.GameScores.GetAllAsync())
                .OrderByDescending(h => h.Value)
                .FirstOrDefault();
            if (score == null)
            {
                return new GameScore() { Value = 0 };
            }

            return score;
        }

        private IRepository<T> GetLocalRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, new LocalGenericRepository<T>(this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
