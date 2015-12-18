namespace BeatPoppin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;
    using Data;
    using BeatPoppin.Commands;
    using BeatPoppin.Pages;

    public class HomeViewModel : BaseViewModel
    {
        private LocalData localData;
        private RemoteData remoteData;
        private long? localPlayerCurrentHighscore;
        private List<StorageFile> playList;
        private string musicPlayList;
        private ICommand chooseMusicCommand;
        private ICommand playMusicCommand;
        private ICommand stopMusicCommand;
        private MediaElement musicPlayer;

        public HomeViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();
            this.playList = new List<StorageFile>();

            this.RefreshLocalScoreAsync();
        }

        public long? LocalPlayerCurrentHighscore
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

        public string MusicPlayList
        {
            get
            {
                return this.musicPlayList;
            }
            private set
            {
                this.musicPlayList = value;
                this.OnPropertyChanged("MusicPlayList");
            }
        }

        private async void ExecChooseMusic()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");
            var result = new StringBuilder();

            var files = await openPicker.PickMultipleFilesAsync();
            if (files != null)
            {
                foreach (var file in files)
                {
                    this.playList.Add(file);
                    result.AppendLine(file.Name);
                }

                this.MusicPlayList = result.ToString();
            }
            else
            {
                this.MusicPlayList = "No selected songs";
            }
        }

        private async void ExecPlayMusic()
        {
            if (this.playList.Count > 0)
            {
                // TODO Grant access
                var storageFile = await StorageFile.GetFileFromPathAsync(this.playList[0].Path);
                var stream = await storageFile.OpenAsync(FileAccessMode.Read);
                if (this.musicPlayer == null)
                {
                    this.musicPlayer = new MediaElement();
                }
                this.musicPlayer.SetSource(stream, storageFile.ContentType);

                this.musicPlayer.Play();
            }
        }

        public async void RefreshLocalScoreAsync()
        {
            var localHighScore = await this.localData.GetCurrentHighScoreAsync();
            this.LocalPlayerCurrentHighscore = localHighScore.Value;
        }

        public async void RefreshRemotScoreAsync()
        {
            var remoteHighScore = await this.remoteData.GetCurrentHighScoreAsync();
            var remoteUserHighScore = await this.remoteData.GetUserForScoreAsync(remoteHighScore);
        }
    }
}
