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
        private List<StorageFile> playlist;
        private ICommand chooseMusicCommand;
        private ICommand playMusicCommand;

        public HomeViewModel()
        {
            this.localData = new LocalData(new LocalDb());
            this.remoteData = new RemoteData();

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

        private async void ExecChooseMusic()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            var files = await openPicker.PickMultipleFilesAsync();
            if (files != null)
            {
                foreach (var file in files)
                {
                    this.playlist.Add(file);
                }
            }
            else
            {
                // Operation cancelled
            }
        }

        private async void ExecPlayMusic()
        {
            var storageFile = await StorageFile.GetFileFromPathAsync(this.playlist[0].Path);
            var stream = await storageFile.OpenAsync(FileAccessMode.Read);

            //mediaElement.SetSource(stream, storageFile.ContentType);

            //mediaElement.Play();
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
