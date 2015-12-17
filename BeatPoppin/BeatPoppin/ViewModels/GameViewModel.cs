namespace BeatPoppin.ViewModels
{
    using Commands;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;

    public class GameViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;

        //ParseObject gameScore = new ParseObject("GameScore");
        //gameScore["score"] = 1337;
        //gameScore["playerName"] = "Sean Plott";
        //await gameScore.SaveAsync();

        private ICommand testRectangleTappedCommand;

        public GameViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();
        }

        private int testHighScoreVal;

        public int testHighScore
        {
            get
            {
                return this.testHighScoreVal;
            }
            set
            {
                this.testHighScoreVal = value;
                this.OnPropertyChanged("HighScore");
            }
        }

        public ICommand TestRectangleTapped
        {
            get
            {
                if (this.testRectangleTappedCommand == null)
                {
                    this.testRectangleTappedCommand = new RelayCommand(this.testRectangleTappedExec);
                }

                return this.testRectangleTappedCommand;
            }
        }

        private async void testRectangleTappedExec()
        {
            //await this.localData.Highscores.AddAsync(new GameScore() { Value = score.Value + 1 });

            //var user = new User()
            //{
            //    DisplayName = "Try2"
            //};
            //await this.remoteData.Users.AddAsync(user);

            //var remoteHighscore = new HighScore() { Value = 100 };
            //await this.remoteData.Highscores.AddAsync(remoteHighscore);

            //await this.remoteData.AddScoreToUserAsync(remoteHighscore, user);

            //var user2 = await this.remoteData.Users.GetByIdAsync("y1zvbIrEuz");

            //var result = await this.remoteData.GetAllScoresForUserAsync(user2);

            //var curr = await this.remoteData.GetCurrentHighScoreAsync();

            //this.HighScore = score.Value;
        }
    }
}
