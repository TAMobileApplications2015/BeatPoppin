namespace BeatPoppin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;
    using Data;
    using BeatPoppin.Commands;
    using Data.Models;
    using Parse;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using System.IO;
    using System.Net.Http;
    public class HomeViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;

        private List<StorageFile> playlist;
        private string musicPlayList;
        private ICommand chooseMusicCommand;
        private ICommand playMusicCommand;
        private ICommand stopMusicCommand;
        private MediaElement musicPlayer;

        private ImageSource imageSource;
        private long localPlayerCurrentHighscore;
        private string remoteHighScoreUserName;
        private string remoteHighScoreValue;

        public HomeViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();
            this.playlist = new List<StorageFile>();
        }

        public long LocalPlayerCurrentHighscore
        {
            get
            {
                return this.localPlayerCurrentHighscore;
            }
            set
            {
                this.localPlayerCurrentHighscore = value;
                this.OnPropertyChanged("LocalPlayerCurrentHighscore");
            }
        }

        public string RemoteHighScoreValue
        {
            get
            {
                return this.remoteHighScoreValue;
            }
            set
            {
                this.remoteHighScoreValue = value;
                this.OnPropertyChanged("RemoteHighScoreValue");
            }
        }

        public string RemoteHighScoreUserName
        {
            get
            {
                return this.remoteHighScoreUserName;
            }
            set
            {
                this.remoteHighScoreUserName = value;
                this.OnPropertyChanged("RemoteHighScoreUserName");
            }
        }

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        public string MusicPlayList
        {
            get
            {
                return this.musicPlayList;
            }
            set
            {
                this.musicPlayList = value;
                this.OnPropertyChanged("MusicPlayList");
            }
        }

        public ICommand ChooseMusic
        {
            get
            {
                if (this.chooseMusicCommand == null)
                {
                    this.chooseMusicCommand = new RelayCommand(this.ExecChooseMusic);
                }

                return this.chooseMusicCommand;
            }
        }

        public ICommand PlayMusic
        {
            get
            {
                if (this.playMusicCommand == null)
                {
                    this.playMusicCommand = new RelayCommand(this.ExecPlayMusic);
                }

                return this.playMusicCommand;
            }
        }

        public ICommand StopMusic
        {
            get
            {
                if (this.stopMusicCommand == null)
                {
                    this.stopMusicCommand = new RelayCommand(this.ExecStopMusic);
                }

                return this.stopMusicCommand;
            }
        }

        private void ExecStopMusic()
        {
            if (this.musicPlayer != null)
            {
                this.musicPlayer.Stop();
            }
        }

        private async void ExecChooseMusic()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".wav");
            openPicker.FileTypeFilter.Add(".wma");
            openPicker.FileTypeFilter.Add(".m4a");

            var fileNames = new StringBuilder();

            var files = await openPicker.PickMultipleFilesAsync();
            if (files != null)
            {
                foreach (var file in files)
                {
                    this.playlist.Add(file);
                    fileNames.AppendLine(file.Name);
                }

                this.MusicPlayList = fileNames.ToString();
            }
            else
            {
                this.MusicPlayList = "No selected songs";
            }
        }

        private async void ExecPlayMusic()
        {
            if (this.playlist.Count > 0)
            {
                // TODO Grant access
                var storageFile = await StorageFile.GetFileFromPathAsync(this.playlist[0].Path);
                var stream = await storageFile.OpenAsync(FileAccessMode.Read);
                if (this.musicPlayer == null)
                {
                    this.musicPlayer = new MediaElement();
                }
                this.musicPlayer.SetSource(stream, storageFile.ContentType);

                this.musicPlayer.Play();
            }
        }

        public async Task<long> RefreshLocalScoreAsync()
        {
            var localHighScore = await this.localData.GetCurrentHighScoreAsync();
            var score = this.LocalPlayerCurrentHighscore = localHighScore.Value;
            return score;
        }

        public async Task<long> RefreshRemoteScoreAsync()
        {
            var remoteHighScore = await this.remoteData.GetCurrentHighScoreAsync();
            this.RemoteHighScoreValue = remoteHighScore.Value.ToString();

            var remoteUserScored = await this.remoteData.GetUserForScoreAsync(remoteHighScore);
            this.RemoteHighScoreUserName = remoteUserScored.Get<string>("displayName");
            var image = remoteUserScored["Image"] as ParseFile;
            this.ImageSource = new BitmapImage(new Uri(image.Url.AbsoluteUri));

            return remoteHighScore.Value;
        }

        public async Task UpdateRemoteFromLocalScore()
        {
            var localHighScore = await this.localData.GetCurrentHighScoreAsync();

            var highScore = new HighScore()
            {
                Value = localHighScore.Value
            };

            var image = new ParseFile(localHighScore.Id + localHighScore.PlayerName + localHighScore.Value, localHighScore.PlayerPhoto);

            var user = new User()
            {
                DisplayName = localHighScore.PlayerName,
                Image = image
            };


            await this.remoteData.Highscores.AddAsync(highScore);
            await this.remoteData.Users.AddAsync(user);
            await this.remoteData.AddScoreToUserAsync(highScore, user);
        }
    }
}
